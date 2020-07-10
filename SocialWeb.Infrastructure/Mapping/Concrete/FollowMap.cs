using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialWeb.Domain.Entities.Concrete;
using SocialWeb.Infrastructure.Mapping.Abstract;

namespace SocialWeb.Infrastructure.Mapping.Concrete
{
    public class FollowMap : BaseMap<Follow>
    {
        public override void Configure(EntityTypeBuilder<Follow> builder)
        {
            builder.HasKey(x => new { x.FollowerId, x.FollowingId });

            base.Configure(builder);
        }
    }
}