using SealMarket.Core.Entities;
using SealMarket.Core.Models.Filters;

namespace SealMarket.Core.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        public Task<List<Category>> GetCategoriesAsync(CategoriesFilter filter);
    }
}
