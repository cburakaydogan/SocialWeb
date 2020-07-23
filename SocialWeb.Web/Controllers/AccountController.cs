using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialWeb.Application.Models.DTOs;
using SocialWeb.Application.Services.Abstract;

namespace SocialWeb.Web.Controllers
{
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private readonly IAppUserService _userservice;

        public AccountController(IAppUserService userservice)
        {
            _userservice = userservice;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region Register
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            return View();
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userservice.Register(model);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

            }

            return View(model);
        }
        #endregion

        #region Login
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _userservice.Login(model);

                if (result.Succeeded)
                {
                    return RedirectToLocal(returnUrl);
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

            return View();
        }
        #endregion

        #region Logout
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _userservice.LogOut();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        #endregion

        #region ExternalLogin

        [AllowAnonymous]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });

            AuthenticationProperties properties = _userservice.ExternalLogin(provider, redirectUrl);

            return new ChallengeResult(provider, properties);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = "/")
        {
            var info = await _userservice.GetExternalLoginInfo();

            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }

            var signInResult = await _userservice.ExternalLoginSignIn(info.LoginProvider, info.ProviderKey);
            if (signInResult.Succeeded)
            {
                return RedirectToLocal(returnUrl);
            }
            else
            {
                ViewData["ReturnUrl"] = returnUrl;
                ViewData["Provider"] = info.LoginProvider;
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                return View("ExternalLoginConfirmation", new ExternalLoginDto { Email = email });
            }
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginDto model, string returnUrl = "/")
        {
            if (!ModelState.IsValid)
                return View(model);

            var info = await _userservice.GetExternalLoginInfo();
            if (info == null)
                return View();

            var result = await _userservice.ExternalRegister(info, model);

            if (result.Succeeded)
            {
                return RedirectToLocal(returnUrl);
            }

            foreach (var error in result.Errors)
            {
                ModelState.TryAddModelError(error.Code, error.Description);
            }

            return View(nameof(ExternalLoginConfirmation), model);
        }

        #endregion

        #region EditProfile
        public async Task<IActionResult> EditProfile(string userName)
        {
            if (userName == User.Identity.Name)
            {
                var user = await _userservice.GetById(User.GetUserId());
                
                if (user == null)
                    return NotFound();

                return View(user);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(EditProfileDto model, IFormFile file)
        {
            model.Image = file;
            await _userservice.EditUser(model);
           return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        #endregion

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}