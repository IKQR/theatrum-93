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
    [Route("admin/theater")]

    [Authorize(Roles = Roles.Admin)]
    public class TheatersAdminController : Controller
    {
        private readonly ITheatrService _theatrService;
        private readonly PaginationConfig _paginationConfig;

        public TheatersAdminController(ITheatrService theatrService,
            IOptions<PaginationConfig> paginationConfig)
        {
            _theatrService = theatrService;
            _paginationConfig = paginationConfig.Value;
        }

        [Route("Index")]
        public async Task<IActionResult> Index(TheatrFilteringAdminModel filteringAdminModel, [FromQuery] int page = 1)
        {
            var result = await Theatrs(filteringAdminModel, page, nameof(Index));
            return View(result);
        }
        private async Task<TheatrFilteringAdminModel> Theatrs(TheatrFilteringAdminModel filteringAdminModel, int page, string actionName)
        {
            if (filteringAdminModel.FilteringSettings == null)
            {
                filteringAdminModel.FilteringSettings = new TheatrFilteringSettingsAdminModel();
            }

            var count = await _theatrService.GetAllCount(filteringAdminModel.FilteringSettings);

            var theatrs =
                await _theatrService.GetAllPaginated(filteringAdminModel.FilteringSettings,
                    _paginationConfig.PaginationAdminPageSize * (page - 1),
                    _paginationConfig.PaginationAdminPageSize);

            TheatrFilteringAdminModel theatrsFilteringAdminModel = new TheatrFilteringAdminModel()
            {
                Theatrs = new GenericPaginatedModel<TheatrModel>()
                {
                    Models = theatrs,
                    Pagination = new PaginationAdminModel(count, page,
                        _paginationConfig.PaginationAdminPageSize, actionName),
                },
                FilteringSettings = filteringAdminModel.FilteringSettings,
            };
            return theatrsFilteringAdminModel;
        }

        [HttpGet]
        [Route("item")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var theatr = await _theatrService.GetById((Guid)id);
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
        public async Task<IActionResult> Create([Bind("Id,Name,Description,City,Address")] TheatrModel theatr)
        {
            if (ModelState.IsValid)
            {
                await _theatrService.CreateOrUpdate(theatr);
                return RedirectToAction(nameof(Index));
            }
            return View(theatr);
        }

        [HttpGet]
        [Route("edit")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theatr = await _theatrService.GetById((Guid)id);
            if (theatr == null)
            {
                return NotFound();
            }
            return View(theatr);
        }

        [HttpPost]
        [Route("edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description,City,Address")] TheatrModel theatr)
        {
            if (id != theatr.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _theatrService.CreateOrUpdate(theatr);
                return RedirectToAction(nameof(Index));
            }
            return View(theatr);
        }

        [HttpGet]
        [Route("delete")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var theatr = await _theatrService.GetById((Guid)id);
            if (theatr == null)
            {
                return NotFound();
            }

            return View(theatr);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _theatrService.DeleteById((Guid)id);
            return RedirectToAction(nameof(Index));
        }
    }
}
