using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SocialWeb.Application.AutoMapper;
using SocialWeb.Application.Services.Abstract;
using SocialWeb.Application.Services.Concrete;
using SocialWeb.Domain.Repositories;
using SocialWeb.Domain.UnitOfWork;
using SocialWeb.Infrastructure.Repositories;
using SocialWeb.Infrastructure.UnitOfWork;

namespace SocialWeb.IoC
{
    public class IocContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Mapping));

            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IFollowRepository, FollowRepository>();
            services.AddScoped<ILikeRepository, LikeRepository>();
            services.AddScoped<IMentionRepository, MentionRepository>();
            services.AddScoped<IShareRepository, ShareRepository>();
            services.AddScoped<ITweetRepository, TweetRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            services.AddScoped<IUserService, UserService>();
        }
    }
}