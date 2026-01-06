using SealMarket.Core.Models.Filters;

namespace SealMarket.Core.Interfaces
{
    public interface ICartRepository : IBaseRepository<Cart>
    {
        public Task<List<Cart>> GetCartsAsync(CartsFilter filter);

        public Task<Cart?> GetCartByAccountAsync(int accountId);

        public Task<Cart?> GetCartWithIncludesAsync(int id);
    }
}
