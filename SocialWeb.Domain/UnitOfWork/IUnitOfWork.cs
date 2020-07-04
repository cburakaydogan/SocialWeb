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
        IUserRepository User { get; }
        IFollowRepository Follow { get; }
        ILikeRepository Like { get; }
        IShareRepository Share { get; }
        Task Commit();
    }
}