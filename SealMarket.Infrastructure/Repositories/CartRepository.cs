using Microsoft.EntityFrameworkCore;
using SealMarket.Core.Interfaces.Repositories;
using SealMarket.Core.Models.Filters;
using SealMarket.Infrastructure.Data;
using static SealMarket.Core.Constans.CartOrderParameters;

namespace SealMarket.Infrastructure.Repositories
{
    public class CartRepository : BaseRepository<Cart>, ICartRepository
    {
        public CartRepository(AppDbContext context) : base(context) { }

        public async Task<List<Cart>> GetCartsAsync(CartsFilter filter)
        {
            var query = _context.Carts
                .Include(c => c.Products)  
                .AsQueryable();
         
            query = query.Where(c => c.Name.Contains(filter.SearchText));
 

            query = query.Where(c => c.Products.Sum(p => p.Price) >= filter.MinTotalPrice
                                  && c.Products.Sum(p => p.Price) <= filter.MaxTotalPrice);

            query = filter.OrderParam switch
            {
                TotalPrice => filter.ByAscending
                    ? query.OrderBy(c => c.Products.Sum(p => p.Price))
                    : query.OrderByDescending(c => c.Products.Sum(p => p.Price)),

                _ => filter.ByAscending
                    ? query.OrderBy(c => c.Name)
                    : query.OrderByDescending(c => c.Name),
            };

            return await query
                .Skip(filter.Size * (filter.Page - 1))
                .Take(filter.Size)
                .ToListAsync();
        }
    }
}
