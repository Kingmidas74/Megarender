using System.Collections.Generic;

namespace Megarender.Domain
{
    public class MoneyTransaction:Entity,IUserCreatable
    {
        public decimal Amount {get;set;}
        public virtual MoneyTransactionStatusId MoneyTransactionStatus {get;set;}
        public virtual User CreatedBy {get;set;}
    }
}