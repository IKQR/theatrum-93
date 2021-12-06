using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using Theatrum.Bl.Abstract.IServices;
using Theatrum.Models.Admin;
using Theatrum.Models.Models;
using Theatrum.Models.Settings;

namespace Theatrum.Web.Razor.Controllers
{
    public class TheaterController : Controller
    {
        private readonly ITheatrService _theatrService;
        private readonly PaginationConfig _paginationConfig;

        public TheaterController(ITheatrService theatrService,
            IOptions<PaginationConfig> paginationConfig)
        {
            _theatrService = theatrService;
            _paginationConfig = paginationConfig.Value;
        }

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
    }
}
