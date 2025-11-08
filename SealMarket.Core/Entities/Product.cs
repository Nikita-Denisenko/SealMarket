using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SealMarket.Core.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public Brand Brand { get; set; } 
        public int BrandId { get; set; }
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; }
        public decimal Price { get; set; } = 0;
        public bool IsActive { get; set; }

        public Product
        (
            int id, 
            string name, 
            int categoryId, 
            Brand brand,
            int brandId, 
            string description, 
            string imageUrl, 
            decimal price, 
            bool isActive
        )
        {
            Id = id;
            Name = name;
            CategoryId = categoryId;
            Brand = brand;
            BrandId = brandId;
            Description = description;
            ImageUrl = imageUrl;
            Price = price;
            IsActive = isActive;
        }
    }
}
