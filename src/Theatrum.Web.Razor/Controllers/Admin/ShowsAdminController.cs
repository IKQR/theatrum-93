using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using Theatrum.Bl.Abstract.IServices;
using Theatrum.Models.Admin;
using Theatrum.Models.Models;
using Theatrum.Models.Settings;
using Theatrum.Utils;

namespace Theatrum.Web.Razor.Controllers.Admin
{
    [Route("admin/theaters")]

    [Authorize(Roles = Roles.Admin)]
    public class ShowsAdminController : Controller
    {
        private readonly IShowService _showService;
        private readonly PaginationConfig _paginationConfig;

        public ShowsAdminController(IShowService showService,
            IOptions<PaginationConfig> paginationConfig)
        {
            _showService = showService;
            _paginationConfig = paginationConfig.Value;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Shows(ShowFilteringAdminModel filteringAdminModel, [FromQuery] int page = 1)
        {
            var result = await Shows(filteringAdminModel, page, nameof(Shows));
            return View(result);
        }

        private async Task<ShowFilteringAdminModel> Shows(ShowFilteringAdminModel filteringAdminModel, int page, string actionName)
        {
            if (filteringAdminModel.FilteringSettings == null)
            {
                filteringAdminModel.FilteringSettings = new ShowFilteringSettingsAdminModel();
            }

            var count = await _showService.GetAllCount(filteringAdminModel.FilteringSettings);

            var shows =
                await _showService.GetAllPaginated(filteringAdminModel.FilteringSettings,
                    _paginationConfig.PaginationAdminPageSize * (page - 1),
                    _paginationConfig.PaginationAdminPageSize);

            ShowFilteringAdminModel showsFilteringAdminModel = new ShowFilteringAdminModel()
            {
                Shows = new GenericPaginatedModel<ShowModel>()
                {
                    Models = shows,
                    Pagination = new PaginationAdminModel(count, page,
                        _paginationConfig.PaginationAdminPageSize, actionName),
                },
                FilteringSettings = filteringAdminModel.FilteringSettings,
            };
            return showsFilteringAdminModel;
        }

        [HttpGet]
        [Route("item")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var theatr = await _showService.GetById((Guid)id);
            return View(theatr);
        }

        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TheatrumId,Name,Description,StartDate,EndDate,PhotoId,AgeLimitation")] ShowModel show)
        {
            if (ModelState.IsValid)
            {
                await _showService.CreateOrUpdate(show);
                return RedirectToAction(nameof(Shows));
            }
            return View(show);
        }

        [HttpGet]
        [Route("edit")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theatr = await _showService.GetById((Guid)id);
            if (theatr == null)
            {
                return NotFound();
            }
            return View(theatr);
        }

        [HttpPost]
        [Route("edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,TheatrumId,Name,Description,StartDate,EndDate,PhotoId,AgeLimitation")] ShowModel show)
        {
            if (id != show.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _showService.CreateOrUpdate(show);
                return RedirectToAction(nameof(Shows));
            }
            return View(show);
        }

        [HttpGet]
        [Route("delete")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var show = await _showService.GetById((Guid)id);
            if (show == null)
            {
                return NotFound();
            }

            return View(show);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _showService.DeleteById((Guid)id);
            return RedirectToAction(nameof(Shows));
        }
    }
}
