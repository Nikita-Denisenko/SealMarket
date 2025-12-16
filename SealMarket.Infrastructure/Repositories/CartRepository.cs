using Microsoft.EntityFrameworkCore;
using SealMarket.Core.Interfaces;
using SealMarket.Core.Models.Filters;
using SealMarket.Infrastructure.Data;
using static SealMarket.Core.Constants.CartOrderParameters;

namespace SealMarket.Infrastructure.Repositories
{
    public class CartRepository : BaseRepository<Cart>, ICartRepository
    {
        public CartRepository(AppDbContext context) : base(context) { }

        public async Task<List<Cart>> GetCartsAsync(CartsFilter filter)
        {
            var cartsWithTotal = _context.Carts
                .Include(c => c.CartItems)
                .Where(c => c.Name.Contains(filter.SearchText))
                .Select(c => new
                {
                    Cart = c,
                    TotalPrice = c.CartItems.Sum(item => item.ProductPrice * item.Quantity)
                })
                .AsQueryable();

            cartsWithTotal = cartsWithTotal.Where(x =>
                x.TotalPrice >= filter.MinTotalPrice &&
                x.TotalPrice <= filter.MaxTotalPrice);

            var orderedQuery = filter.OrderParam switch
            {
                TotalPrice => filter.ByAscending
                    ? cartsWithTotal.OrderBy(x => x.TotalPrice)
                    : cartsWithTotal.OrderByDescending(x => x.TotalPrice),

                _ => filter.ByAscending
                    ? cartsWithTotal.OrderBy(x => x.Cart.Name)
                    : cartsWithTotal.OrderByDescending(x => x.Cart.Name),
            };

            return await orderedQuery
                .Select(x => x.Cart)
                .Skip(filter.Size * (filter.Page - 1))
                .Take(filter.Size)
                .ToListAsync();
        }
    }
}