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
        private readonly IFollowService _followService;
        private readonly IAppUserService _appUserService;

        public TweetService(IUnitOfWork unitOfWork, IMapper mapper, IFollowService followService , IAppUserService appUserService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _followService = followService;
            _appUserService = appUserService;
        }

        public async Task<List<UsersTweetsVm>> UsersTweets(string userName,int pageIndex)
        {
            var userId = await _appUserService.UserIdFromName(userName);
            var tweets = await _unitOfWork.Tweet.GetFilteredList(
                selector: y => new UsersTweetsVm
                {
                    Id = y.Id,
                    Text = y.Text,
                    ImagePath = y.ImagePath,
                    AppUserId = y.AppUserId,
                    LikesCount = y.Likes.Count,
                    MentionsCount = y.Mentions.Count,
                    SharesCount = y.Shares.Count,
                    CreateDate = y.CreateDate,
                    UserName = y.AppUser.Name,
                    UserImage = y.AppUser.ImagePath,
                    Name = y.AppUser.Name,
                    isLiked = y.Likes.Any(z => z.AppUserId == userId)
                },
                orderBy: z => z.OrderByDescending(x => x.CreateDate),
                predicate: x => x.AppUserId == userId,
                include: x => x
              .Include(z => z.AppUser)
              .ThenInclude(z => z.Followers)
              .Include(z => z.Likes),
              pageIndex: pageIndex);

            // I did not use Automapper in this projection query because of generic repository pattern. 

            //var tweets = await _context.Tweets
            //   .Include(x => x.AppUser)
            //   .Include(x => x.Likes)
            //   .Where(x => x.AppUserId == userId);
            //   .ProjectTo<UsersTweetsVm>(_mapper.ConfigurationProvider, new { userId })
            //   .OrderByDescending(x => x.CreateDate).ToListAsync();

            return tweets;

        }

        public async Task<List<TimelineVm>> GetTimeline(int userId,int pageIndex)
        {
            List<int> followings = await _followService.FollowingList(userId);

            var tweets = await _unitOfWork.Tweet.GetFilteredList(
                 selector: y => new TimelineVm
                 {
                     Id = y.Id,
                     Text = y.Text,
                     ImagePath = y.ImagePath,
                     AppUserId = y.AppUserId,
                     LikesCount = y.Likes.Count,
                     MentionsCount = y.Mentions.Count,
                     SharesCount = y.Shares.Count,
                     CreateDate = y.CreateDate,
                     UserName = y.AppUser.Name,
                     UserImage = y.AppUser.ImagePath,
                     Name = y.AppUser.Name,
                     isLiked = y.Likes.Any(z => z.AppUserId == userId)
                 },
                 orderBy: z => z.OrderByDescending(x => x.CreateDate),
                 predicate: x => followings.Contains(x.AppUserId),
                 include: x => x
               .Include(z => z.AppUser)
               .ThenInclude(z => z.Followers)
               .Include(z => z.Likes),
               pageIndex: pageIndex);

            //var tweets = await _context.Tweets
            //   .Include(x => x.AppUser).ThenInclude(x => x.Followers)
            //   .Include(x => x.Likes)
            //   .ProjectTo<TimelineVm>(_mapper.ConfigurationProvider, new { userId })
            //   .Where(x => followings.Contains(x.AppUserId))
            //   .OrderByDescending(x => x.CreateDate).ToListAsync();

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
            var tweet = await _unitOfWork.Tweet.GetFilteredFirstorDefault(
                selector: y => new TweetDetailVm
                {
                    Id = y.Id,
                    Text = y.Text,
                    ImagePath = y.ImagePath,
                    AppUserId = y.AppUserId,
                    LikesCount = y.Likes.Count,
                    MentionsCount = y.Mentions.Count,
                    SharesCount = y.Shares.Count,
                    CreateDate = y.CreateDate,
                    UserName = y.AppUser.Name,
                    UserImage = y.AppUser.ImagePath,
                    Name = y.AppUser.Name,
                    Mentions = y.Mentions.Where(z => z.TweetId == y.Id).OrderByDescending(z => z.CreateDate).Select(x => new MentionDto
                    {
                        Id = x.Id,
                        Text = x.Text,
                        AppUserId = x.AppUserId,
                        UserName = x.AppUser.UserName,
                        Name = x.AppUser.Name,
                        TweetId = x.TweetId,
                        CreateDate = x.CreateDate,
                        UserImage = x.AppUser.ImagePath
                    }).ToList(),
                    isLiked = y.Likes.Any(z => z.AppUserId == userId)
                },
                orderBy: z => z.OrderByDescending(x => x.CreateDate),
                predicate: x => x.Id == id,
                include: x => x
              .Include(z => z.AppUser)
              .ThenInclude(z => z.Followers)
              .Include(z => z.Likes));


            //var tweet = await _context.Tweets.Where(x => x.Id == id)
            //.Include(x => x.Likes)
            //.Include(x => x.AppUser)
            //.Include(x => x.Mentions).ProjectTo<TweetDetailVm>(_mapper.ConfigurationProvider, new { userId }).FirstOrDefaultAsync();

            return tweet;
        }

    }
}