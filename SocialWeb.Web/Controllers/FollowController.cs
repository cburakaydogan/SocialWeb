using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            var claimsIdentity = (ClaimsIdentity) User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (!model.isExist)
            {
                if (model.FollowerId == Convert.ToInt32(claim.Value))
                {
                    await _followService.Follow(model);
                    return Json("Success");
                }
                else
                {
                    return Json("Failed");
                }
            }
            else
            {
                if (model.FollowerId == Convert.ToInt32(claim.Value))
                {
                    await _followService.Unfollow(model);
                    return Json("Success");
                }
                else
                {
                    return Json("Failed");
                }
            }

        }
    }
}