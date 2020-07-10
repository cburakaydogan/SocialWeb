using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialWeb.Domain.Entities.Concrete;
using SocialWeb.Infrastructure.Mapping.Concrete;
using System;

namespace SocialWeb.Infrastructure.Context
{
    public class ApplicationDbContext : IdentityDbContext<AppUser,AppRole,int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Tweet> Tweets { get; set; }
        public DbSet<Mention> Mentions { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Share> Shares { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new TweetMap());
            modelBuilder.ApplyConfiguration(new MentionMap());
            modelBuilder.ApplyConfiguration(new AppUserMap());
            modelBuilder.ApplyConfiguration(new FollowMap());
            modelBuilder.ApplyConfiguration(new LikeMap());
            modelBuilder.ApplyConfiguration(new ShareMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}