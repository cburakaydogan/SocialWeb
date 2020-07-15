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
    public class LikeController : Controller
    {
        private readonly ILikeService _likeService;

        public LikeController(ILikeService likeService)
        {
            _likeService = likeService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Like([FromBody] LikeDto model)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (model.AppUserId == Convert.ToInt32(claim.Value))
            {
                await _likeService.Like(model);
                return Json("Success");
            }
            return Json("Failed");
        }

        [HttpPost]
        public async Task<IActionResult> Unlike([FromBody] LikeDto model)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (model.AppUserId == Convert.ToInt32(claim.Value))
            {
                await _likeService.Unlike(model);
                return Json("Success");
            }
            return Json("Failed");
        }
    }
}
