using System;
using System.Linq;
using Megarender.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Megarender.DataAccess
{
    class MoneyTransactionStatusConfiguration : IEntityTypeConfiguration<MoneyTransactionStatus>
    {
        public virtual void Configure(EntityTypeBuilder<MoneyTransactionStatus> builder)
        {
            builder.HasData (
                Enum.GetValues (typeof (MoneyTransactionStatusId))
                .Cast<MoneyTransactionStatusId> ()
                .Select (e => new MoneyTransactionStatus
                {
                        MoneyTransactionStatusId = e,
                        Value = e.ToString ()
                })
            );    
        }
    }
}