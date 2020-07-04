using System;
using System.Threading.Tasks;
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
        public ITweetRepository Tweet
        {
            get { return _tweetRepository ?? (_tweetRepository = new TweetRepository(_db)); }
        }

        private IMentionRepository _mentionRepository;
        public IMentionRepository Mention
        {
            get { return _mentionRepository ?? (_mentionRepository = new MentionRepository(_db)); }
        }

        private IUserRepository _userRepository;
        public IUserRepository User
        {
            get { return _userRepository ?? (_userRepository = new UserRepository(_db)); }
        }

        private IFollowRepository _followRepository;
        public IFollowRepository Follow
        {
            get { return _followRepository ?? (_followRepository = new FollowRepository(_db)); }
        }

        private ILikeRepository _likeRepository;
        public ILikeRepository Like
        {
            get { return _likeRepository ?? (_likeRepository = new LikeRepository(_db)); }
        }

        private IShareRepository _shareRepository;

        public IShareRepository Share
        {
            get { return _shareRepository ?? (_shareRepository = new ShareRepository(_db)); }
        }

        public async Task Commit()
        {
            await _db.SaveChangesAsync();
        }

        private bool isDisposed = false;

        public void Dispose()
        {
            if (!isDisposed)
            {
                isDisposed = true;
                Dispose(true);
                GC.SuppressFinalize(this);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
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