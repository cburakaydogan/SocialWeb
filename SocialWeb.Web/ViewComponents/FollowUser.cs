using Microsoft.AspNetCore.Mvc;
using SocialWeb.Application.Models.DTOs;
using SocialWeb.Application.Services.Abstract;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SocialWeb.Web.ViewComponents
{
    public class FollowUser : ViewComponent
    {
        private readonly IAppUserService _userservice;
        private readonly IFollowService _followService;

        public FollowUser(IAppUserService service, IFollowService followService)
        {
            _userservice = service;
            _followService = followService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string userName)
        {
            int userId = await _userservice.UserIdFromName(userName);

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            int followerId = Convert.ToInt32(claim.Value);

            var followDto = new FollowDto { FollowerId = followerId, FollowingId = userId };

           followDto.isExist = await _followService.isFollowing(followDto);

            return View(followDto);
        }
    }
}