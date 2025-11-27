using SealMarket.Core.Interfaces.Repositories;
using SealMarket.Core.Interfaces.Services;

namespace SealMarket.Application.Services
{
    public class CartService : BaseService<Cart>, ICartService
    {
        public CartService(IBaseRepository<Cart> repo) : base(repo) { }
    }
}
