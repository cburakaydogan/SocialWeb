using SocialWeb.Application.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialWeb.Application.Services.Abstract
{
    public interface ITweetService
    {
        Task<List<TimelineVm>> getTimeline(int userId);
        Task AddTweet(SendTweetDto model);
        Task<TweetDetailVm> TweetDetail(int id, int userId);
    }
}
