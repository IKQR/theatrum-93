using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Theatrum.Entities.Entities;
using Theatrum.Utils;
using Theatrum.Web.Razor.Models;

namespace Theatrum.Web.Razor.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        [Route("/login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [Route("/login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel credentials, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid || credentials == null)
            {
                return View(credentials);
            }

            var identityUser = await _userManager.FindByEmailAsync(credentials.Email);

            if (identityUser == null)
            {
                return View(credentials);
            }


            var result = _userManager.PasswordHasher.VerifyHashedPassword(identityUser, identityUser.PasswordHash, credentials.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                return View(credentials);
            }

            var roles = await _userManager.GetRolesAsync(identityUser);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, identityUser.Id.ToString()),
                new Claim(ClaimTypes.Name, identityUser.UserName),
                new Claim(ClaimTypes.Role, roles[0]),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            return RedirectToLocal(returnUrl);
        }

        [HttpGet]
        [Route("/register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [Route("/register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel credentials, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid || credentials == null)
            {
                return View(credentials);
            }

            if (credentials.ConfirmPassword != credentials.Password)
            {
                ModelState.AddModelError(nameof(credentials.ConfirmPassword), "Passwords must be equal");
                return View(credentials);
            }
            
            if (credentials.Birthday > DateTimeOffset.Now.Subtract(new TimeSpan(365 * 14 + 10, 0, 0, 0, 0)))
            {
                ModelState.AddModelError(nameof(credentials.Birthday), "You are too young for this)");
                return View(credentials);
            }

            AppUser identityUser = await _userManager.FindByEmailAsync(credentials.Email);

            if (identityUser != null)
            {
                ModelState.AddModelError(nameof(credentials.Email), "Such user alredy exists");
                return View(credentials);
            }


            AppUser user = new AppUser()
            {
                Email = credentials.Email,
                EmailConfirmed = true,
                UserName = credentials.Name,
            };

            IdentityResult result = await _userManager.CreateAsync(user, credentials.Password);

            if (result != IdentityResult.Success)
            {
                return View(credentials);
            }
            await _userManager.AddToRoleAsync(user, Roles.User);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, Roles.User),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
            return RedirectToLocal(returnUrl);
        }


        [Route("/logout")]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Response.Cookies.Delete(".AspNetCore.Cookies");
            _logger.LogInformation("User logged out.");
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
    }
}
