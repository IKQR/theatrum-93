using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Theatrum.Utils;

namespace Theatrum.Web.Razor.Controllers.Admin
{
    [Route("admin")]
    [Authorize(Roles = Roles.Admin)]
    public class AdminController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
