using Megarender.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Megarender.DataAccess
{
    class PrivateMoneyTransactionConfiguration : MoneyTransactionConfiguration<PrivateMoneyTransaction>
    {
        public override void Configure(EntityTypeBuilder<PrivateMoneyTransaction> builder)
        {
            base.Configure(builder);
        }
    }
}