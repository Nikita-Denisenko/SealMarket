using SealMarket.Core.Entities;
using SealMarket.Core.Models.Filters;

namespace SealMarket.Core.Interfaces.Repositories
{
    public interface IBrandRepository : IBaseRepository<Brand>
    {
        public Task<List<Brand>> GetBrandsAsync(BrandsFilter filter);
    }
}
