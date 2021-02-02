using System.Collections.Generic;

namespace Megarender.Domain
{
    public record MoneyTransaction:Entity,IUserCreatable
    {
        public decimal Amount {get; init;}
        public virtual MoneyTransactionStatusId MoneyTransactionStatus {get; init;}
        public virtual User CreatedBy {get; init;}
    }
}