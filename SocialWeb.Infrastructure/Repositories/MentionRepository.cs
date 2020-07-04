using SocialWeb.Domain.Entities.Concrete;
using SocialWeb.Domain.Repositories;
using SocialWeb.Infrastructure.Context;

namespace SocialWeb.Infrastructure.Repositories
{
    public class MentionRepository: BaseRepository<Mention>, IMentionRepository
    {
        public MentionRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}