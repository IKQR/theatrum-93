using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

namespace Theatrum.Web.Razor.Controllers
{
    [Route("/show")]
    public class ShowsController : Controller
    {
        private readonly IShowService _showService;
        private readonly PaginationConfig _paginationConfig;

        public ShowsController(IShowService showService,
            IOptions<PaginationConfig> paginationConfig)
        {
            _showService = showService;
            _paginationConfig = paginationConfig.Value;
        }

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
    }
}
