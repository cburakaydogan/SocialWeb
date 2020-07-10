using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialWeb.Domain.Entities.Concrete;
using SocialWeb.Infrastructure.Mapping.Abstract;

namespace SocialWeb.Infrastructure.Mapping.Concrete
{
    public class ShareMap: BaseMap<Share>
    {
        public override void Configure(EntityTypeBuilder<Share> builder)
        {
            builder.HasKey(x => new { x.TweetId , x.AppUserId });

            base.Configure(builder);
        }
    }
}