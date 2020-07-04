using SocialWeb.Domain.Entities.Concrete;
using SocialWeb.Domain.Repositories;
using SocialWeb.Infrastructure.Context;

namespace SocialWeb.Infrastructure.Repositories
{
    public class FollowRepository:BaseRepository<Follow>, IFollowRepository
    {
        public FollowRepository(ApplicationDbContext context):base(context)
        {
            
        }
    }
}