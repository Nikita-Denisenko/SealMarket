namespace SealMarket.Core.Entities
{
    public class CartItem
    {
        public int Id { get; private set; }
        public int ProductId { get; private set; }
        public Product Product { get; private set; }
        public int CartId { get; private set; }
        public Cart Cart { get; private set; }
        public int Quantity { get; private set; }
        public DateTime AddedAt { get; private set; }
        public decimal ProductPrice { get; private set; }
        public decimal TotalPrice => Quantity * ProductPrice;
        private CartItem(){ }

        public CartItem(int productId, int cartId, int quantity, decimal productPrice) 
        { 
            ProductId = productId;
            CartId = cartId;
            Quantity = quantity;
            ProductPrice = productPrice;
            AddedAt = DateTime.UtcNow;
        }
    }
}
