namespace Megarender.Domain
{
    public record SharedMoneyTransaction:MoneyTransaction
    {
        public virtual Organization Organization {get; init;}
    }
}