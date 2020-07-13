using SocialWeb.Application.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialWeb.Application.Services.Abstract
{
    public interface IFollowService
    {
        Task Follow(FollowDto model);
        Task Unfollow(FollowDto model);
        Task<bool> isFollowing(FollowDto model);
    }
}
