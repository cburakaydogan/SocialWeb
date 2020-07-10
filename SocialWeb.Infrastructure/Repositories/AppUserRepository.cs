using SocialWeb.Domain.Entities.Concrete;
using SocialWeb.Domain.Repositories;
using SocialWeb.Infrastructure.Context;

namespace SocialWeb.Infrastructure.Repositories
{
    public class AppUserRepository: BaseRepository<AppUser>, IAppUserRepository
    {
        public AppUserRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}