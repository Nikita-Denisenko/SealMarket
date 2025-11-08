using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SealMarket.Core.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Product> Products { get; set; } = [];
        public decimal TotalPrice { get; set; } = 0;
    }
}
