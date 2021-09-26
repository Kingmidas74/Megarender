using Megarender.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Megarender.DataAccess
{
    class MoneyTransactionConfiguration<TEntity> : UserCreatableConfiguration<TEntity>
                                                where TEntity: MoneyTransaction
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);
            builder.Property(e=>e.Amount).IsRequired();            
            builder.Property(e=>e.MoneyTransactionStatus)
                    .HasConversion<int> ();
        }
    }
}