using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SocialWeb.Application.Models.DTOs;
using SocialWeb.Application.Services.Abstract;
using SocialWeb.Domain.Entities.Concrete;
using SocialWeb.Domain.UnitOfWork;
using SocialWeb.Infrastructure.Context;

namespace SocialWeb.Application.Services.Concrete
{
    public class TweetService : ITweetService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly IFollowService _followService;

        public TweetService(IUnitOfWork unitOfWork, IMapper mapper, ApplicationDbContext context, IFollowService followService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
            _followService = followService;
        }

        public async Task<List<UsersTweetsVm>> UsersTweets(int userId)
        {
            var tweets = await _context.Tweets
                .Include(x => x.AppUser)
                .Include(x => x.Likes)
                .ProjectTo<UsersTweetsVm>(_mapper.ConfigurationProvider, new { userId })
                .OrderByDescending(x => x.CreateDate).ToListAsync();

            return tweets;

        }

        public async Task<List<TimelineVm>> GetTimeline(int userId)
        {
            List<int> followings = await _followService.FollowingList(userId);

            var tweets = await _context.Tweets
                .Include(x => x.AppUser).ThenInclude(x => x.Followers)
                .Include(x => x.Likes)
                .ProjectTo<TimelineVm>(_mapper.ConfigurationProvider, new { userId })
                .Where(x => followings.Contains(x.AppUserId))
                .OrderByDescending(x => x.CreateDate).ToListAsync();

            return tweets;

        }

        public async Task AddTweet(SendTweetDto model)
        {

            if (model.Image != null)
            {
                using var image = Image.Load(model.Image.OpenReadStream());
                image.Mutate(x => x.Resize(7500, 750));
                image.Save("wwwroot/images/tweets/" + Guid.NewGuid() + ".jpg");
                model.ImagePath = ("/images/tweets/" + Guid.NewGuid() + ".jpg");
            }

            var tweet = _mapper.Map<SendTweetDto, Tweet>(model);

            await _unitOfWork.Tweet.Add(tweet);
            await _unitOfWork.Commit();
        }

        public async Task<TweetDetailVm> TweetDetail(int id, int userId)
        {

            var tweet = await _context.Tweets.Where(x => x.Id == id)
                .Include(x => x.Likes)
                .Include(x => x.AppUser)
                .Include(x => x.Mentions).ProjectTo<TweetDetailVm>(_mapper.ConfigurationProvider, new { userId }).FirstOrDefaultAsync();
                
            return tweet;
        }

    }
}