﻿using SocialWeb.Application.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialWeb.Application.Services.Abstract
{
    public interface IFollowService
    {
        Task Follow(FollowDto model);
        Task Unfollow(FollowDto model);
        Task<bool> isFollowing(FollowDto model);
        Task<List<int>> FollowingList(int id);
        Task<List<int>> FollowerList(int id);

    }
}
