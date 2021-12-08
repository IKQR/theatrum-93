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
    [Route("admin/users")]

    [Authorize(Roles = Roles.Admin)]
    public class UsersAdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly PaginationConfig _paginationConfig;

        public UsersAdminController(IUserService userService,
            IOptions<PaginationConfig> paginationConfig)
        {
            _userService = userService;
            _paginationConfig = paginationConfig.Value;
        }

        [Route("Index")]
        public async Task<IActionResult> Index(UserFilteringAdminModel filteringAdminModel, [FromQuery] int page = 1)
        {
            var result = await Theatrs(filteringAdminModel, page, nameof(Index));
            return View(result);
        }
        private async Task<UserFilteringAdminModel> Theatrs(UserFilteringAdminModel filteringAdminModel, int page, string actionName)
        {
            if (filteringAdminModel.FilteringSettings == null)
            {
                filteringAdminModel.FilteringSettings = new UserFilteringSettingsAdminModel();
            }

            var count = await _userService.GetAllCount(filteringAdminModel.FilteringSettings);

            var users =
                await _userService.GetAllPaginated(filteringAdminModel.FilteringSettings,
                    _paginationConfig.PaginationAdminPageSize * (page - 1),
                    _paginationConfig.PaginationAdminPageSize);

            UserFilteringAdminModel theatrsFilteringAdminModel = new UserFilteringAdminModel()
            {
                Users = new GenericPaginatedModel<AppUserModel>()
                {
                    Models = users,
                    Pagination = new PaginationAdminModel(count, page,
                        _paginationConfig.PaginationAdminPageSize, actionName),
                },
                FilteringSettings = filteringAdminModel.FilteringSettings,
            };
            return theatrsFilteringAdminModel;
        }

        [HttpGet]
        [Route("delete")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var theatr = await _userService.GetById((Guid)id);
            if (theatr == null)
            {
                return NotFound();
            }

            return View(theatr);
        }

        [HttpPost, Route("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _userService.DeleteById((Guid)id);
            return RedirectToAction(nameof(Index));
        }
    }
}
