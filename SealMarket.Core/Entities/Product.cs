namespace SealMarket.Core.Entities
{
    public class Product
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public Brand Brand { get; private set; }
        public int BrandId { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public string ImageUrl { get; private set; } = string.Empty;
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsActive { get; private set; }
        
        private Product() { }

        public Product(
            string name,
            int brandId,
            string description,
            string imageUrl,
            int quantity,
            decimal price,
            bool isActive = true
        )
        {
            Name = name;
            BrandId = brandId;       
            Description = description;
            ImageUrl = imageUrl;
            Quantity = quantity;
            Price = price;
            CreatedAt = DateTime.UtcNow;
            IsActive = isActive;
        }

        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice < 0)
                throw new ArgumentException("Price cannot be negative");
            Price = newPrice;
        }

        public void UpdateQuantity(int newQuantity)
        {
            if (newQuantity < 0)
                throw new ArgumentException("Quantity cannot be negative");
            Quantity = newQuantity;
        }

        public void Activate() => IsActive = true;
        public void Deactivate() => IsActive = false;

        public void ReduceQuantity(int amount)
        {
            if (amount > Quantity)
                throw new InvalidOperationException("Not enough quantity in stock");
            Quantity -= amount;
        }

        public void IncreaseQuantity(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be positive");
            Quantity += amount;
        }
    }
}