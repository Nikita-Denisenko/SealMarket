namespace SealMarket.Core.Entities
{
    public class Notification
    {
        public int Id { get; private set; }
        public int AccountId { get; private set; }
        public Account? Account { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Message { get; private set; } = string.Empty;
        public DateTime DateTime { get; private set; }
        public bool HasBeenRead { get; private set; }
        
        private Notification() { }

        public Notification
        (
            int accountId,
            string message,
            string name
        )
        {
            Message = message;
            DateTime = DateTime.UtcNow;
            HasBeenRead = false;
            AccountId = accountId;
            Name = name;
        }

        public void MarkAsRead()
        {
            if(!HasBeenRead)
                HasBeenRead = true;
        }
    }
}
