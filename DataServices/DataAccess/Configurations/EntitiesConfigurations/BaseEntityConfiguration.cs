using Megarender.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Megarender.DataAccess
{
    class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
                                        where TEntity:class, IEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(e=>e.Id);
            builder.Property(e=>e.Status)
                    .HasConversion<int> ();
        }
    }
}