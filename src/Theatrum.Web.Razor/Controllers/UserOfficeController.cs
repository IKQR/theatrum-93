using System.Linq;
using System.Threading.Tasks;

using Mapster;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Theatrum.Entities.Entities;
using Theatrum.Utils;
using Theatrum.Web.Razor.Models;

namespace Theatrum.Web.Razor.Controllers
{
    [Authorize(Roles = Roles.User)]
    public class UserOfficeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public UserOfficeController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            AppUser user = (await _userManager.GetUsersInRoleAsync(Roles.User)).FirstOrDefault(x => x.UserName == User.Identity.Name);

            RegisterViewModel m = new RegisterViewModel
            {
                Name = user.UserName,
                Birthday = user.BirthdayDate,
                Email = user.Email,
            };

            return View(m);
        }
    }
}
