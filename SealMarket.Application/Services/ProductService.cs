using SealMarket.Core.Interfaces.Repositories;
using SealMarket.Core.Interfaces.Services;

namespace SealMarket.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;

        public ProductService(IProductRepository repo) 
        { 
            _repo = repo;
        }
    }
}
