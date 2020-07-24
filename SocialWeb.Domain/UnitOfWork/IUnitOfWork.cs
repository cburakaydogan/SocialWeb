using System;
using System.Threading.Tasks;
using SocialWeb.Domain.Repositories;

namespace SocialWeb.Domain.UnitOfWork
{
    public interface IUnitOfWork: IAsyncDisposable
    {
        ITweetRepository Tweet { get; }
        IMentionRepository Mention { get; }
        IAppUserRepository AppUser { get; }
        IFollowRepository Follow { get; }
        ILikeRepository Like { get; }
        IShareRepository Share { get; }
        Task Commit();
        Task ExecuteSqlRaw(string sql, params object[] parameters);
    }
}