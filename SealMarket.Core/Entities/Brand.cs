namespace SealMarket.Core.Entities
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string LogoUrl { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<Product> Products { get; set; } 

        public Brand
        (
            string name,
            string displayName,
            string logoUrl,
            string description,
            List<Product> products
        )
        {
            Name = name;
            DisplayName = displayName;
            LogoUrl = logoUrl;
            Description = description;
            Products = products;
        }
    }
}
