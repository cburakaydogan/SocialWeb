using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SocialWeb.Application.AutoMapper;
using SocialWeb.Application.Services.Abstract;
using SocialWeb.Application.Services.Concrete;
using SocialWeb.Domain.Entities.Concrete;
using SocialWeb.Domain.UnitOfWork;
using SocialWeb.Infrastructure.Context;
using SocialWeb.Infrastructure.UnitOfWork;

namespace SocialWeb.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Mapping));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAppUserService, AppUserService>();
            services.AddScoped<IFollowService, FollowService>();
            services.AddScoped<ILikeService, LikeService>();
            services.AddScoped<ITweetService, TweetService>();
            services.AddScoped<IMentionService, MentionService>();
            
            services.AddIdentity<AppUser, AppRole>(x =>
            {
                x.SignIn.RequireConfirmedPhoneNumber = false;
                x.SignIn.RequireConfirmedAccount = false;
                x.SignIn.RequireConfirmedEmail = false;
                x.User.RequireUniqueEmail = true;
                x.Password.RequiredLength = 1;
                x.Password.RequiredUniqueChars = 0;
                x.Password.RequireUppercase = false;
                x.Password.RequireNonAlphanumeric = false;
                x.Password.RequireLowercase = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>();
            return services;
        }
    }
}