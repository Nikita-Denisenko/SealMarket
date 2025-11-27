using SealMarket.Core.Entities;
using SealMarket.Core.Models.Filters;

namespace SealMarket.Core.Interfaces.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        public Task<List<Product>> GetProductsAsync(ProductsFilter filter);
    }
}
