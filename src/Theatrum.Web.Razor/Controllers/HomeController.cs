using System;
using System.Diagnostics;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Theatrum.Bl.Abstract.IServices;
using Theatrum.Models.Admin;
using Theatrum.Models.Models;
using Theatrum.Models.Settings;
using Theatrum.Web.Razor.Models;

namespace Theatrum.Web.Razor.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IShowService _showService;
        private readonly PaginationConfig _paginationConfig;

        public HomeController(
            ILogger<HomeController> logger,
            IShowService showService,
            IOptions<PaginationConfig> paginationConfig)
        {
            _logger = logger;
            _showService = showService;
            _paginationConfig = paginationConfig.Value;
        }

        [Route("")]
        public async Task<IActionResult> Index(ShowFilteringAdminModel filteringAdminModel, [FromQuery] int page = 1)
        {
            var result = await Shows(filteringAdminModel, page, nameof(Shows));
            return View(result);
        }

        [Route("ShowsByTheater")]
        public async Task<IActionResult> ShowsByTheater(Guid theaterId)
        {
            var result = await Shows(new ShowFilteringAdminModel()
            {
                FilteringSettings = new ShowFilteringSettingsAdminModel()
                {
                    TheatrId = theaterId,
                }
            }, 1, nameof(Shows));
            return View("Index", result);
        }

        private async Task<ShowFilteringAdminModel> Shows(ShowFilteringAdminModel filteringAdminModel, int page, string actionName)
        {
            if (filteringAdminModel.FilteringSettings == null)
            {
                filteringAdminModel.FilteringSettings = new ShowFilteringSettingsAdminModel();
            }

            var count = await _showService.GetAllCount(filteringAdminModel.FilteringSettings);

            var shows =
                await _showService.GetActualPaginated(filteringAdminModel.FilteringSettings,
                    _paginationConfig.PaginationAdminPageSize * (page - 1),
                    _paginationConfig.PaginationAdminPageSize);

            ShowFilteringAdminModel showsFilteringAdminModel = new ShowFilteringAdminModel()
            {
                Shows = new GenericPaginatedModel<ShowModel>()
                {
                    Models = shows,
                    Pagination = new PaginationAdminModel(count, page,
                        _paginationConfig.PaginationAdminPageSize, actionName, "Home"),
                },
                FilteringSettings = filteringAdminModel.FilteringSettings,
            };
            return showsFilteringAdminModel;
        }

        [Route("/privacy")]
        public IActionResult Privacy()
        {
            return View();
        }


        [Route("/error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
