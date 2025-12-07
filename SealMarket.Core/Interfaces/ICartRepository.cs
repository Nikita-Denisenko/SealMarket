using SealMarket.Core.Models.Filters;

namespace SealMarket.Core.Interfaces
{
    public interface ICartRepository : IBaseRepository<Cart>
    {
        public Task<List<Cart>> GetCartsAsync(CartsFilter filter);
    }
}
