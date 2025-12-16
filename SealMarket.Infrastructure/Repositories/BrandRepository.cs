using Microsoft.EntityFrameworkCore;
using SealMarket.Core.Entities;
using SealMarket.Core.Interfaces;
using SealMarket.Core.Models.Filters;
using SealMarket.Infrastructure.Data;
using static SealMarket.Core.Constants.BrandOrderParameters;

namespace SealMarket.Infrastructure.Repositories
{
    public class BrandRepository : BaseRepository<Brand>, IBrandRepository
    {
        public BrandRepository(AppDbContext context) : base(context) { }

        public async Task<List<Brand>> GetBrandsAsync(BrandsFilter filter)
        {
            var brandsWithStats = _context.Brands
                .Include(b => b.Products)
                .Where(b => b.Name.Contains(filter.SearchText))
                .Select(b => new
                {
                    Brand = b,
                    AveragePrice = b.Products.Average(p => p.Price),
                    ProductsCount = b.Products.Count
                })
                .AsQueryable();

            brandsWithStats = brandsWithStats.Where(x =>
                x.AveragePrice >= filter.MinAverageProductPrice &&
                x.AveragePrice <= filter.MaxAverageProductPrice);

            var orderedQuery = filter.OrderParam switch
            {
                AverageProductPrice => filter.ByAscending
                    ? brandsWithStats.OrderBy(x => x.AveragePrice)
                    : brandsWithStats.OrderByDescending(x => x.AveragePrice),

                ProductsQuantity => filter.ByAscending
                    ? brandsWithStats.OrderBy(x => x.ProductsCount)
                    : brandsWithStats.OrderByDescending(x => x.ProductsCount),

                _ => filter.ByAscending
                    ? brandsWithStats.OrderBy(x => x.Brand.Name)
                    : brandsWithStats.OrderByDescending(x => x.Brand.Name),
            };

            return await orderedQuery
                .Select(x => x.Brand)
                .Skip(filter.Size * (filter.Page - 1))
                .Take(filter.Size)
                .ToListAsync();
        }
    }
}