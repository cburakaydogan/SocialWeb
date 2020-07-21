using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        public async Task<IActionResult> AddTweet(SendTweetDto model)
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

        public async Task<IActionResult> TweetDetail(int id)
        {

            var claimsIdentity = (ClaimsIdentity) User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            int userId = Convert.ToInt32(claim.Value);

            var tweet = await _tweetService.TweetDetail(id, userId);

            return View(tweet);
        }

        [HttpPost]
        public async Task<IActionResult> GetTweets(int pageIndex, int pageSize, string userName = null)
        {
            
            if (userName == null){

                var claimsIdentity = (ClaimsIdentity) User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                int userId = Convert.ToInt32(claim.Value);

                var tweets = await _tweetService.GetTimeline(userId, pageIndex);

                return Json(tweets, new JsonSerializerSettings());
            }

            else{
                var tweets = await _tweetService.UsersTweets(userName, pageIndex);

                return Json(tweets, new JsonSerializerSettings());
            }
            
        }

    }
}