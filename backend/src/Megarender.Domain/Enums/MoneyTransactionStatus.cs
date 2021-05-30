namespace Megarender.Domain
{
    public enum MoneyTransactionStatusId
    {
        Created = 1,
        Approved = 2,
        Deposited = 3, 
        Declined = 4, 
        Reversed = 5, 
        Refunded = 6
    }
    public class MoneyTransactionStatus {
        public MoneyTransactionStatusId MoneyTransactionStatusId { get; set; }
        public string Value { get; set; }
    }
}