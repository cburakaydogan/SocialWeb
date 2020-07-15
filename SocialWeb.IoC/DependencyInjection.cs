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
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Mapping));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAppUserService, AppUserService>();
            services.AddScoped<IFollowService, FollowService>();
            services.AddScoped<ITweetService, TweetService>();

            return services;
        }
    }
}