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
        public string Name { get; set; }
    }
}
