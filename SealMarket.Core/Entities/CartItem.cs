namespace SealMarket.Core.Entities
{
    public class CartItem
    {
        public int Id { get; private set; }
        public int ProductId { get; private set; }
        public Product? Product { get; private set; }
        public int CartId { get; private set; }
        public Cart? Cart { get; private set; }
        public int Quantity { get; private set; }
        public DateTime AddedAt { get; private set; }
        public decimal? TotalPrice => Quantity * Product?.Price;
        private CartItem(){ }

        public CartItem
        (
            int productId, 
            int cartId, 
            int quantity
        ) 
        { 
            ProductId = productId;
            CartId = cartId;
            Quantity = quantity;
            AddedAt = DateTime.UtcNow;
        } 

        public void IncreaseQuantity(int amount = 1)
        {
            if (amount < 1)
                throw new ArgumentException("Amount must be at least 1.", nameof(amount));
            Quantity += amount;
        }

        public void DecreaseQuantity(int amount = 1)
        {
            if (amount < 1)
                throw new ArgumentException("Amount must be at least 1.", nameof(amount));
            if (Quantity - amount < 0)
                throw new InvalidOperationException("Quantity cannot be negative.");
            Quantity -= amount;
        }
    }
}
