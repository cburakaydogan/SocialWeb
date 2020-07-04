using SocialWeb.Domain.Entities.Concrete;
using SocialWeb.Domain.Repositories;
using SocialWeb.Infrastructure.Context;

namespace SocialWeb.Infrastructure.Repositories
{
    public class TweetRepository: BaseRepository<Tweet>, ITweetRepository
    {
        public TweetRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}