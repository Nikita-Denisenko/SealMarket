using Microsoft.EntityFrameworkCore;
using SealMarket.Core.Entities;
using SealMarket.Core.Interfaces.Repositories;
using SealMarket.Core.Models.Filters;
using SealMarket.Infrastructure.Data;
using static SealMarket.Core.Constans.BrandOrderParameters;

namespace SealMarket.Infrastructure.Repositories
{
    public class BrandRepository : BaseRepository<Brand>, IBrandRepository
    {
        public BrandRepository(AppDbContext context) : base(context) { }

        public async Task<List<Brand>> GetBrandsAsync(BrandsFilter filter)
        {
            var query = _context.Brands
                .Include(b => b.Products) 
                .AsQueryable();


            query = query
                .Where(b => b.Name.Contains(filter.SearchText));

            query = query.Where(b => b.Products.Average(p => p.Price) >= filter.MinAverageProductPrice
                                  && b.Products.Average(p => p.Price) <= filter.MaxAverageProductPrice);

            query = filter.OrderParam switch
            {
                AverageProductPrice => filter.ByAscending
                    ? query.OrderBy(b => b.Products.Average(p => p.Price))
                    : query.OrderByDescending(b => b.Products.Average(p => p.Price)),

                ProductsQuantity => filter.ByAscending
                    ? query.OrderBy(b => b.Products.Count)
                    : query.OrderByDescending(b => b.Products.Count),

                _ => filter.ByAscending
                    ? query.OrderBy(b => b.Name)
                    : query.OrderByDescending(b => b.Name),
            };

            return await query
                .Skip(filter.Size * (filter.Page - 1))
                .Take(filter.Size)
                .ToListAsync();
        }
    }
}
