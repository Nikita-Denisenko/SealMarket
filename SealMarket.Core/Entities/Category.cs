namespace SealMarket.Core.Entities
{
    public class Category
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public string ImageUrl { get; private set; } = string.Empty;
        public List<Product> Products { get; private set; } = [];
        private Category() {}
        public Category
        (
            string name,
            string description
        )
        {
            Name = name;
            Description = description;
        }
        public void UpdateInfo
        (
            string name,
            string description,
            string imageUrl
        )
        {
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
        }
    }
}
