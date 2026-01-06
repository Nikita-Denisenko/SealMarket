namespace SealMarket.Core.Entities
{
    public class Transaction
    {
        public int Id { get; private set; }
        public Account? Account { get; private set; }
        public int AccountId { get; private set; }
        public decimal TotalSum { get; private set; }
        public bool IsSuccessful { get; private set; }
        public string Message { get; private set; } = string.Empty;
        public DateTime CreatedAt { get; private set; }

        public Transaction
        (
            int accountId,
            decimal totalSum,
            bool isSuccessful,
            string message
        )    
        { 
            AccountId = accountId;
            TotalSum = totalSum;
            IsSuccessful = isSuccessful;
            Message = message;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
