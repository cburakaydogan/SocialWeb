using SocialWeb.Application.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialWeb.Application.Services.Abstract
{
    public interface ITweetService
    {
        Task<List<TimelineVm>> GetTimeline(int userId,int pageIndex);
        Task AddTweet(SendTweetDto model);
        Task<TweetDetailVm> TweetDetail(int id, int userId);
        Task<List<TimelineVm>> UsersTweets(string userName,int id ,int pageIndex);
        Task DeleteTweet(int id, int userId);
    }
}
