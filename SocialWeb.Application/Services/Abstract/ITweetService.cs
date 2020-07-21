using SocialWeb.Application.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialWeb.Application.Services.Abstract
{
    public interface ITweetService
    {
        Task<List<TimelineVm>> GetTimeline(int userId,int pageIndex);
        Task AddTweet(SendTweetDto model);
        Task<TweetDetailVm> TweetDetail(int id, int userId);
        Task<List<UsersTweetsVm>> UsersTweets(string userName ,int pageIndex);
    }
}
