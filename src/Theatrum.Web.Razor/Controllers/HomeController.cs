using System.Diagnostics;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Theatrum.Models.Models;
using Theatrum.Web.Razor.Models;

namespace Theatrum.Web.Razor.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Route("")]
        public async Task<IActionResult> Index([FromQuery]int page = 1)
        {
            return View(new PaginationViewModel<ShowModel>
            {
                Options = new PaginationOptionsModel(10, page, "Index", "Home"),
                Models = { },
            });
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
