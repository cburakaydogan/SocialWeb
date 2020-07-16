using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SocialWeb.Application.Services.Abstract;

namespace SocialWeb.Web.ViewComponents
{
    public class Timeline: ViewComponent
    {
        private readonly ITweetService _tweetService;

        public Timeline(ITweetService service)
        {
            _tweetService = service;
        }
        public async Task<IViewComponentResult> InvokeAsync(string userName)
        {
           var claimsIdentity = (ClaimsIdentity) User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            int userId = Convert.ToInt32(claim.Value);

            var tweets = await _tweetService.getTimeline(userId);

            return View(tweets);
        }
    }
}
