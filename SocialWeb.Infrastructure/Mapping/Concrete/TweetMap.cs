using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialWeb.Domain.Entities.Concrete;
using SocialWeb.Infrastructure.Mapping.Abstract;

namespace SocialWeb.Infrastructure.Mapping.Concrete
{
    public class TweetMap : BaseMap<Tweet>
    {
        public override void Configure(EntityTypeBuilder<Tweet> builder)
        {

            builder.HasKey(x => x.Id);

            builder.Property(x=> x.Text).HasColumnType("varchar(280)").IsRequired();
            builder.Property(x=> x.ImagePath).HasColumnType("varchar(100)").IsRequired(false);

            builder.HasMany(x => x.Mentions)
                .WithOne(x => x.Tweet)
                .HasForeignKey(x => x.TweetId);

            builder.HasMany(x => x.Likes)
                .WithOne(x => x.Tweet)
                .HasForeignKey(x => x.TweetId);

            builder.HasMany(x => x.Shares)
                .WithOne(x => x.Tweet)
                .HasForeignKey(x => x.TweetId);

            base.Configure(builder);
        }
    }
}