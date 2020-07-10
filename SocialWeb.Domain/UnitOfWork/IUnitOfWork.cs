using System;
using System.Threading.Tasks;
using SocialWeb.Domain.Entities.Concrete;
using SocialWeb.Domain.Repositories;

namespace SocialWeb.Domain.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable, IDisposable
    {
        ITweetRepository Tweet { get; }
        IMentionRepository Mention { get; }
        IAppUserRepository AppUser { get; }
        IFollowRepository Follow { get; }
        ILikeRepository Like { get; }
        IShareRepository Share { get; }
        Task Commit();
        void ExecuteSqlRaw(string sql, params object[] parameters);
    }
}