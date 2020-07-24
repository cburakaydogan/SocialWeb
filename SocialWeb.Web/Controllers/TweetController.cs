using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SocialWeb.Application.Models.DTOs;
using SocialWeb.Application.Services.Abstract;

namespace SocialWeb.Web.Controllers
{
    [Authorize]
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

        [HttpPost]
        public async Task<IActionResult> AddTweet(SendTweetDto model)
        {
            if (ModelState.IsValid)
            {
                if (model.AppUserId == User.GetUserId())
                {
                    await _tweetService.AddTweet(model);
                    return Json("Success");
                }
                else
                {
                    return Json("Failed");
                }
            }
            else
            {
                return BadRequest(String.Join(Environment.NewLine, ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage + " " + v.Exception)));
            }
        }

        public async Task<IActionResult> TweetDetail(int id)
        {

            var tweet = await _tweetService.TweetDetail(id, User.GetUserId()); //userid for isLiked
            return View(tweet);
        }

        [HttpPost]
        public async Task<IActionResult> GetTweets(int pageIndex, int pageSize, string userName=null)
        {

            if (userName == null){

                var tweets = await _tweetService.GetTimeline(User.GetUserId(), pageIndex);

                return Json(tweets, new JsonSerializerSettings());
            }

            else
            {
                var tweets = await _tweetService.UsersTweets(userName,User.GetUserId(), pageIndex);

                return Json(tweets, new JsonSerializerSettings());
            }

        }
        [HttpPost]
        public async Task<IActionResult> DeleteTweet(int id)
        {
            await _tweetService.DeleteTweet(id, User.GetUserId());

            return Json("");
        }

    }
}