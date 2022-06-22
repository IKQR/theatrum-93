using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;

using Theatrum.Bl.Abstract.IServices;
using Theatrum.Models.Admin;
using Theatrum.Models.Models;
using Theatrum.Models.Settings;
using Theatrum.Utils;

namespace Theatrum.Web.Razor.Controllers.Admin
{
    [Route("admin/show")]

    [Authorize(Roles = Roles.Admin)]
    public class ShowsAdminController : Controller
    {
        private readonly IShowService _showService;
        private readonly ITheatrService _theatrService;
        private readonly PaginationConfig _paginationConfig;

        public ShowsAdminController(IShowService showService,
            ITheatrService theatrService,
            IOptions<PaginationConfig> paginationConfig)
        {
            _showService = showService;
            _theatrService = theatrService;
            _paginationConfig = paginationConfig.Value;
        }

        [Route("Shows")]
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
        public async Task<IActionResult> Create()
        {
            var theatersForSelect = await _theatrService.GetAllForSelect();
            ViewBag.TheatrumId = theatersForSelect.Select(x => new SelectListItem(x.Item2, x.Item1.ToString()));

            return View();
        }

        [HttpPost]
        [Route("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm]ShowModel show)
        {
            if (ModelState.IsValid)
            {
                if (show.StartDate > DateTimeOffset.Now)
                {
                    if (show.StartDate < show.EndDate)
                    {
                        await _showService.CreateOrUpdate(show);
                        return RedirectToAction(nameof(Shows));
                    }
                    else
                    {
                        ModelState.AddModelError("StartDate", "Дата початку повинна бути раніше, аніж дата кінця");
                    }
  
                }
                else
                {
                    ModelState.AddModelError("StartDate", "Дата початку повинна бути пізніше, аніж сьогоднішня дата");
                }
            }

            var theatersForSelect = await _theatrService.GetAllForSelect();
            ViewBag.TheatrumId = theatersForSelect.Select(x => new SelectListItem(x.Item2, x.Item1.ToString()));

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

            var theatersForSelect = await _theatrService.GetAllForSelect();
            ViewBag.TheatrumId = theatersForSelect.Select(x => new SelectListItem(x.Item2, x.Item1.ToString()));
            return View(theatr);
        }

        [HttpPost]
        [Route("edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [FromForm] ShowModel show)
        {
            if (id != show.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    if (show.StartDate > DateTimeOffset.Now)
                    {
                        if (show.StartDate < show.EndDate)
                        {
                            await _showService.CreateOrUpdate(show);
                            return RedirectToAction(nameof(Shows));
                        }
                        else
                        {
                            ModelState.AddModelError("StartDate", "Дата початку повинна бути раніше, аніж дата кінця");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("StartDate", "Дата початку повинна бути пізніше, аніж сьогоднішня дата");
                    }
                }
            }
            var theatersForSelect = await _theatrService.GetAllForSelect();
            ViewBag.TheatrumId = theatersForSelect.Select(x => new SelectListItem(x.Item2, x.Item1.ToString()));
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

        [HttpPost, Route("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _showService.DeleteById((Guid)id);
            return RedirectToAction(nameof(Shows));
        }
    }
}
