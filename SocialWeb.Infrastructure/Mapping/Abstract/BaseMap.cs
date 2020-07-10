using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialWeb.Domain.Entities.Abstract;

namespace SocialWeb.Infrastructure.Mapping.Abstract
{
    public abstract class BaseMap<T> : IEntityTypeConfiguration<T> where T : class, IBaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.Status).IsRequired(true);
            builder.Property(x => x.CreateDate).IsRequired(true).HasDefaultValue(System.DateTime.Now);
            builder.Property(x => x.ModifiedDate).IsRequired(false);
            builder.Property(x => x.DeletedDate).IsRequired(false);
        }

    }
}