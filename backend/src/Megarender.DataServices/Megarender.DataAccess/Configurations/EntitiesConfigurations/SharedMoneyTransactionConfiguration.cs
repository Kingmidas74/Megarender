using Megarender.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Megarender.DataAccess
{
    class SharedMoneyTransactionConfiguration : MoneyTransactionConfiguration<SharedMoneyTransaction>
    {
        public override void Configure(EntityTypeBuilder<SharedMoneyTransaction> builder)
        {
            base.Configure(builder);
            builder.Property ($"{nameof(Organization)}{nameof(Organization.Id)}");            
            builder.HasOne(c => c.Organization)
                .WithMany ()
                .HasForeignKey ($"{nameof(Organization)}{nameof(Organization.Id)}")
                .IsRequired();                      
        }
    }
}