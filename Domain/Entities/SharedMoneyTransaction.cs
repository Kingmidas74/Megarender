using System.Collections.Generic;

namespace Megarender.Domain
{
    public class SharedMoneyTransaction:MoneyTransaction
    {
        public virtual Organization Organization {get;set;}
    }
}