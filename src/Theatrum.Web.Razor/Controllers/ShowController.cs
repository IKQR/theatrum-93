using Microsoft.AspNetCore.Mvc;

namespace Theatrum.Web.Razor.Controllers
{
    [Route("/show")]
    public class ShowController : Controller
    {
        [Route("{id}")]
        public IActionResult Index([FromRoute]string id)
        {
            return View();
        }
    }
}
