using SealMarket.Core.Models.Filters;

namespace SealMarket.Core.Interfaces.Repositories
{
    public interface ICartRepository : IBaseRepository<Cart>
    {
        public Task<List<Cart>> GetCartsAsync(CartsFilter filter);
    }
}
