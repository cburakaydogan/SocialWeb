using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SocialWeb.Application.Models.DTOs;
using SocialWeb.Application.Services.Abstract;

namespace SocialWeb.Web.Controllers
{
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class ProfileController : Controller
    {
        private readonly IAppUserService _appUserService;
        public ProfileController(IAppUserService appUserService)
        {
            _appUserService = appUserService;

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(string userName)
        {
            ViewBag.userName = userName;
            return View();
        }
        public IActionResult Followings(string userName)
        {

            ViewBag.userName = userName;
            return View();
        }

        public IActionResult Followers(string userName)
        {
            ViewBag.userName = userName;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Followers(string userName, int pageIndex)
        {
            var findUser = await _appUserService.UserIdFromName(userName);

            if (findUser > 0)
            {
                var followers = await _appUserService.UsersFollowers(findUser, pageIndex);

                return Json(followers, new JsonSerializerSettings());
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Followings(string userName, int pageIndex)
        {
            var findUser = await _appUserService.UserIdFromName(userName);

            if (findUser > 0)
            {
                var followings = await _appUserService.UsersFollowings(findUser, pageIndex);

                return Json(followings, new JsonSerializerSettings());
            }
            else
            {
                return NotFound();
            }

        }

    }
}