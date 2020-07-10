using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialWeb.Domain.Entities.Concrete;
using SocialWeb.Infrastructure.Mapping.Abstract;

namespace SocialWeb.Infrastructure.Mapping.Concrete
{
    public class MentionMap : BaseMap<Mention>
    {
        public override void Configure(EntityTypeBuilder<Mention> builder)
        {

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Text).HasColumnType("varchar(280)").IsRequired();

            base.Configure(builder);
        }
    }
}