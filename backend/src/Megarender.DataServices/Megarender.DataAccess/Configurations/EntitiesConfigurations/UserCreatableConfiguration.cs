using Megarender.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Megarender.DataAccess
{
    class UserCreatableConfiguration<TEntity> : BaseEntityConfiguration<TEntity>
                                        where TEntity:class, IUserCreatable, IEntity
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);
            builder.HasOne(c => c.CreatedBy)
                    .WithMany ()
                    .HasForeignKey ($"{nameof(IUserCreatable.CreatedBy)}{nameof(IEntity.Id)}")
                    .IsRequired();
        }
    }
}