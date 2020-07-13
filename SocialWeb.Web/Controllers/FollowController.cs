using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialWeb.Application.Models.DTOs;
using SocialWeb.Application.Services.Abstract;

namespace SocialWeb.Web.Controllers
{
    [Authorize]
    public class FollowController : Controller
    {
        private readonly IFollowService _followService;

        public FollowController(IFollowService followService)
        {
            _followService = followService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Follow(FollowDto model)
        {
            await _followService.Follow(model);

           return RedirectToAction("Index", "home");
        }

        [HttpPost]
        public async Task<IActionResult> UnFollow(FollowDto model)
        {
            await _followService.Unfollow(model);

            return RedirectToAction("Index", "home");
        }
    }
}
