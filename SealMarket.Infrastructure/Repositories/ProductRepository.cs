using Microsoft.EntityFrameworkCore;
using SealMarket.Core.Entities;
using SealMarket.Core.Interfaces;
using SealMarket.Core.Models.Filters;
using SealMarket.Infrastructure.Data;
using static SealMarket.Core.Constants.ProductOrderParameters;

namespace SealMarket.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }

        public async Task<List<Product>> GetProductsAsync(ProductsFilter filter)
        {
            var query = _context.Products
                .Include(p => p.Category)
                .AsQueryable();

            query = query
                .Where(query => query.Category.Name == filter.CategoryName);

            query = query
                .Where(p => p.Name.Contains(filter.SearchText));

            query = query.
                Where(p => p.Price >= filter.MinPrice && p.Price <= filter.MaxPrice);

            query = filter.OrderParam switch
            {
                Quantity => filter.ByAscending
                    ? query.OrderBy(p => p.Quantity)
                    : query.OrderByDescending(p => p.Quantity),

                Price => filter.ByAscending
                    ? query.OrderBy(p => p.Price)
                    : query.OrderByDescending(p => p.Price),
                
                DateCreated => filter.ByAscending
                    ? query.OrderBy(p => p.CreatedAt)
                    : query.OrderByDescending(p => p.CreatedAt),

                _ => filter.ByAscending
                   ? query.OrderBy(p => p.Name)
                   : query.OrderByDescending(p => p.Name),
            };

            query = query
                .Skip(filter.Size * (filter.Page - 1))
                .Take(filter.Size);

            return await query.ToListAsync();
        }

        public async Task<Product?> GetWithBrandByIdAsync(int id)    
           => await _context.Products
                .Include(p => p.Brand)
                .FirstOrDefaultAsync(p => p.Id == id); 
    }
}
