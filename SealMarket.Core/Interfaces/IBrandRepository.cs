using SealMarket.Core.Entities;
using SealMarket.Core.Models.Filters;

namespace SealMarket.Core.Interfaces
{
    public interface IBrandRepository : IBaseRepository<Brand>
    {
        public Task<List<Brand>> GetBrandsAsync(BrandsFilter filter);

        public Task<Brand?> GetWithProductsAsync(int id);
    }
}
