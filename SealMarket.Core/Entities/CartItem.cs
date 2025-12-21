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
        public decimal TotalPrice => Quantity * Product.Price;
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
    }
}
