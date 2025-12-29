using Microsoft.EntityFrameworkCore;
using SealMarket.Core.Entities;
using SealMarket.Core.Interfaces;
using SealMarket.Core.Models.Filters;
using SealMarket.Infrastructure.Data;
using static SealMarket.Core.Constants.CategoryOrderParameters;

namespace SealMarket.Infrastructure.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context) { }

        public async Task<List<Category>> GetCategoriesAsync(CategoriesFilter filter)
        {
            var query = _context.Categories
                .Include(c => c.Products)
                .AsQueryable();

            query = query
                .Where(c => c.Name.Contains(filter.SearchText));

            query = filter.OrderParam switch
            {
                ProductsQuantity => filter.ByAscending
                    ? query.OrderBy(c => c.Products.Count)
                    : query.OrderByDescending(c => c.Products.Count),

                _ => filter.ByAscending
                    ? query.OrderBy(c => c.Name)
                    : query.OrderByDescending(c => c.Name),
            };

            query = query
                 .Skip((filter.Page - 1) * filter.Size)
                 .Take(filter.Size);
            
            return await query.ToListAsync();
        }
    }
}
