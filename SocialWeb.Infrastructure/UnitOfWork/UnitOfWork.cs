using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialWeb.Domain.Repositories;
using SocialWeb.Domain.UnitOfWork;
using SocialWeb.Infrastructure.Context;
using SocialWeb.Infrastructure.Repositories;

namespace SocialWeb.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            this._db = db ??
                throw new ArgumentNullException("db can't be null");
        }
        private ITweetRepository _tweetRepository;
        public ITweetRepository Tweet {get { return _tweetRepository ?? (_tweetRepository = new TweetRepository(_db)); }}

        private IMentionRepository _mentionRepository;
        public IMentionRepository Mention { get { return _mentionRepository ?? (_mentionRepository = new MentionRepository(_db)); }}

        private IAppUserRepository _appuserRepository;
        public IAppUserRepository AppUser { get { return _appuserRepository ?? (_appuserRepository = new AppUserRepository(_db)); } }

        private IFollowRepository _followRepository;
        public IFollowRepository Follow { get { return _followRepository ?? (_followRepository = new FollowRepository(_db)); } }

        private ILikeRepository _likeRepository;
        public ILikeRepository Like  { get { return _likeRepository ?? (_likeRepository = new LikeRepository(_db)); } }

        private IShareRepository _shareRepository;
        public IShareRepository Share { get { return _shareRepository ?? (_shareRepository = new ShareRepository(_db)); } }

        public async Task ExecuteSqlRaw(string sql, params object[] parameters)
        {
            await _db.Database.ExecuteSqlRawAsync(sql, parameters);
        }
        private bool isDisposed = false;

        public async Task Commit()
        {
            
            
                    await _db.SaveChangesAsync();
            
        }

        public async ValueTask DisposeAsync()
        {
            if (!isDisposed)
            {
                isDisposed = true;
                await DisposeAsync(true);
                GC.SuppressFinalize(this);
            }
        }

        protected async ValueTask DisposeAsync(bool disposing)
        {
            if (disposing)
            {
                await _db.DisposeAsync();
            }
        }
    }
}