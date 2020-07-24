﻿using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialWeb.Application.Models.DTOs;
using SocialWeb.Application.Services.Abstract;

namespace SocialWeb.Web.Controllers
{
    [Authorize]
    [AutoValidateAntiforgeryToken]
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
            model.AppUserId = User.GetUserId();

            await _likeService.Like(model);

            return Json("Success");
        }

        [HttpPost]
        public async Task<IActionResult> Unlike([FromBody] LikeDto model)
        {
            model.AppUserId = User.GetUserId();

            await _likeService.Unlike(model);

            return Json("Success");
        }
    }
}