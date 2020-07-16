using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SocialWeb.Application.Models.DTOs;
using SocialWeb.Application.Services.Abstract;

namespace SocialWeb.Web.Controllers
{
    public class TweetController : Controller
    {
        private readonly ITweetService _tweetService;

        public TweetController(ITweetService tweetService)
        {
            _tweetService = tweetService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddTweet(TweetDto model)
        {
            var claimsIdentity = (ClaimsIdentity) User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            int userId = Convert.ToInt32(claim.Value);
            if (model.AppUserId == userId)
            {
                await _tweetService.AddTweet(model);
                return Json("Success");
            }
            else
            {
                return Json("Failed");
            }

        }
    }
}