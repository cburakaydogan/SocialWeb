using AutoMapper;
using SocialWeb.Application.Models.DTOs;
using SocialWeb.Domain.Entities.Concrete;

namespace SocialWeb.Application.AutoMapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {

            CreateMap<AppUser, RegisterDto>().ReverseMap();
            CreateMap<AppUser, LoginDto>().ReverseMap();
            CreateMap<AppUser, ExternalLoginDto>().ReverseMap();
            CreateMap<AppUser, EditProfileDto>().ReverseMap();
            CreateMap<AppUser, ProfileSummaryDto>().ReverseMap();
            CreateMap<AppUser, SearchUserDto>().ReverseMap();

            CreateMap<Follow, FollowDto>().ReverseMap();

            CreateMap<Like, LikeDto>().ReverseMap();

            CreateMap<Tweet, SendTweetDto>().ReverseMap();

            //int userId = 0;
            //CreateMap<Tweet, TimelineVm>()
            //    .ForMember(d => d.Name, opt => opt.MapFrom(s => s.AppUser.Name))
            //    .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.AppUser.UserName))
            //    .ForMember(d => d.UserImage, opt => opt.MapFrom(s => s.AppUser.ImagePath))
            //    .ForMember(d => d.isLiked, opt => opt.MapFrom(s => s.Likes.Any(z => z.AppUserId == userId && z.TweetId == z.TweetId)))
            //    .ReverseMap();

            //CreateMap<Tweet, TweetDetailVm>()
            //    .ForMember(d => d.Name, opt => opt.MapFrom(s => s.AppUser.Name))
            //    .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.AppUser.UserName))
            //    .ForMember(d => d.UserImage, opt => opt.MapFrom(s => s.AppUser.ImagePath))
            //    .ForMember(d => d.isLiked, opt => opt.MapFrom(s => s.Likes.Any(z => z.AppUserId == userId && z.TweetId == z.TweetId)))
            //    .ForMember(d => d.Mentions, opt => opt.MapFrom(s => s.Mentions))
            //    .ReverseMap();

            //CreateMap<Tweet, UsersTweetsVm>()
            //    .ForMember(d => d.Name, opt => opt.MapFrom(s => s.AppUser.Name))
            //    .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.AppUser.UserName))
            //    .ForMember(d => d.UserImage, opt => opt.MapFrom(s => s.AppUser.ImagePath))
            //    .ForMember(d => d.isLiked, opt => opt.MapFrom(s => s.Likes.Any(z => z.AppUserId == userId && z.TweetId == z.TweetId)))
            //    .ReverseMap();

            CreateMap<Mention, MentionDto>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.AppUser.Name))
                .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.AppUser.UserName))
                .ForMember(d => d.UserImage, opt => opt.MapFrom(s => s.AppUser.ImagePath))
                .ReverseMap();

              CreateMap<Mention, AddMentionDto>().ReverseMap();
              

        }
    }
}