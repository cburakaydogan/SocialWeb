using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using SocialWeb.Application.Models.DTOs;

namespace SocialWeb.Web.ViewComponents
{
    public class AddTweet:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            int userId = Convert.ToInt32(claim.Value);
            var tweet = new SendTweetDto();
            tweet.AppUserId=userId;
            return View(tweet);
        }
    }
}