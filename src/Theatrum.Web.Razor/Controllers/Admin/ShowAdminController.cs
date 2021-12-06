using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Theatrum.Utils;

namespace Theatrum.Web.Razor.Controllers.Admin
{
    [Route("admin/show")]
    [Authorize(Roles = Roles.Admin)]
    public class ShowAdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
