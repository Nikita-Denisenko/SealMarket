namespace SealMarket.Core.Entities
{
    public class Product
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public Brand? Brand { get; private set; }
        public int BrandId { get; private set; }
        public Category? Category { get; private set; }
        public int CategoryId { get; private set; }
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
            int categoryId,
            string description,
            string imageUrl,
            int quantity,
            decimal price,
            bool isActive = true
        )
        {
            Name = name;
            BrandId = brandId;
            CategoryId = categoryId;
            Description = description;
            ImageUrl = imageUrl;
            Quantity = quantity;
            Price = price;
            CreatedAt = DateTime.UtcNow;
            IsActive = isActive;
        }

        public void UpdateInfo
        (
            string name,
            string description, 
            string imageUrl,
            int quantity,
            decimal price,
            bool isActive
        )
        {
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
            Quantity = quantity;
            Price = price;
            IsActive = isActive;
        }
    }
}