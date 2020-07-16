using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
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

        public TweetService(IUnitOfWork unitOfWork, IMapper mapper, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<TimelineDto>> getTimeline(int userId)
        {
            var tweets = await _context.Tweets.Include(x => x.Likes).Join(_context.AppUsers,
                tweet => tweet.AppUserId,
                user => user.Id,
                (tweet, user) => new { tweet, user }).Join(_context.Follows,
                y => y.user.Id,
                follow => follow.FollowingId,
                (y, follow) => new { y, follow }).Select(x => new TimelineDto
            {
                    Id = x.y.tweet.Id,
                    Text = x.y.tweet.Text,
                    ImagePath = x.y.tweet.ImagePath,
                    AppUserId = x.y.tweet.AppUserId,
                    LikeCount = x.y.tweet.Likes.Count,
                    MentionCount = x.y.tweet.Mentions.Count,
                    ShareCount = x.y.tweet.Shares.Count,
                    TweetDate = x.y.tweet.CreateDate,
                    UserName = x.y.user.Name,
                    UserImage = x.y.user.ImagePath,
                    Name = x.y.user.Name,
                    TimelineOwnerId = x.follow.FollowerId,
                    isLiked = x.y.tweet.Likes.Any(z => z.AppUserId == userId && z.TweetId == x.y.tweet.Id)

            }).Where(x => x.TimelineOwnerId == userId).OrderByDescending(x => x.TweetDate).ToListAsync();

            return tweets;

        }

        public async Task AddTweet(TweetDto model)
        {

            if (model.Image != null)
            {
                using var image = Image.Load(model.Image.OpenReadStream());
                image.Mutate(x => x.Resize(7500, 750));
                image.Save("wwwroot/images/tweets/" + Guid.NewGuid() + ".jpg");
                model.ImagePath = ("/images/tweets/" + Guid.NewGuid() + ".jpg");
            }

            var tweet = _mapper.Map<TweetDto, Tweet>(model);

            await _unitOfWork.Tweet.Add(tweet);
            await _unitOfWork.Commit();
        }
    }
}