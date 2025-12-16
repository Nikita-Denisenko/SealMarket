namespace SealMarket.Core.Entities
{
    public class Brand
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string LogoUrl { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public List<Product> Products { get; private set; } = [];

        private Brand() {}

        public Brand
        (
            string name,
            string logoUrl,
            string description
        )
        {
            Name = name;
            LogoUrl = logoUrl;
            Description = description;
        }
    }
}
