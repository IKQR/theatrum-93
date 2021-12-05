using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Theatrum.Bl.Abstract.IServices;
using Theatrum.Dal.Impl.Postgres;
using Theatrum.Entities.Entities;
using Theatrum.Models.Admin;
using Theatrum.Models.Models;
using Theatrum.Models.Settings;
using Theatrum.Utils;

namespace Theatrum.Web.Razor.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class TheatrsController : Controller
    {
        private readonly ITheatrService _theatrService;
        private readonly PaginationConfig _paginationConfig;

        public TheatrsController(ITheatrService theatrService,
            IOptions<PaginationConfig> paginationConfig)
        {
            _theatrService = theatrService;
            _paginationConfig = paginationConfig.Value;
        }

        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> TheatrsForAdmin(TheatrFilteringAdminModel filteringAdminModel, [FromQuery] int page = 1)
        {
            var result = await Theatrs(filteringAdminModel, page, nameof(TheatrsForAdmin));
            return View(result);
        }

        public async Task<IActionResult> TheatrsForUser(TheatrFilteringAdminModel filteringAdminModel, [FromQuery] int page = 1)
        {
            var result = await Theatrs(filteringAdminModel, page, nameof(TheatrsForUser));
            return View(result);
        }
        [Authorize(Roles = Roles.Admin)]
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

        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var theatr = await _theatrService.GetById((Guid)id);
            return View(theatr);
        }

        [Authorize(Roles = Roles.Admin)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,City,Address")] TheatrModel theatr)
        {
            if (ModelState.IsValid)
            {
                await _theatrService.CreateOrUpdate(theatr);
                return RedirectToAction(nameof(TheatrsForAdmin));
            }
            return View(theatr);
        }

        [Authorize(Roles = Roles.Admin)]
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
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description,City,Address")] TheatrModel theatr)
        {
            if (id != theatr.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _theatrService.CreateOrUpdate(theatr);
                return RedirectToAction(nameof(TheatrsForAdmin));
            }
            return View(theatr);
        }

        [Authorize(Roles = Roles.Admin)]
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _theatrService.DeleteById((Guid)id);
            return RedirectToAction(nameof(TheatrsForAdmin));
        }
    }
}
