using SocialWeb.Domain.Entities.Concrete;
using SocialWeb.Domain.Repositories;
using SocialWeb.Infrastructure.Context;

namespace SocialWeb.Infrastructure.Repositories
{
    public class UserRepository: BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}