using SocialWeb.Domain.Entities.Concrete;
using SocialWeb.Domain.Repositories;
using SocialWeb.Infrastructure.Context;

namespace SocialWeb.Infrastructure.Repositories
{
    public class ShareRepository: BaseRepository<Share>, IShareRepository
    {
        public ShareRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}