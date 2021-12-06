using Microsoft.AspNetCore.Mvc;

namespace Theatrum.Web.Razor.Controllers
{
    public class UserOfficeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
