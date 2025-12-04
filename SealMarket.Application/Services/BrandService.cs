using SealMarket.Core.Interfaces.Repositories;
using SealMarket.Core.Interfaces.Services;

namespace SealMarket.Application.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _repo;

        public BrandService(IBrandRepository repo) 
        { 
            _repo = repo;
        }
    }
}
