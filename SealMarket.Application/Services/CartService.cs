using SealMarket.Core.Interfaces.Repositories;
using SealMarket.Core.Interfaces.Services;

namespace SealMarket.Application.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _repo;

        public CartService(ICartRepository repo) 
        {
            _repo = repo;
        }
    }
}
