using SealMarket.Core.Entities;
using SealMarket.Core.Interfaces.Repositories;
using SealMarket.Core.Interfaces.Services;

namespace SealMarket.Application.Services
{
    public class BrandService : BaseService<Brand>, IBrandService
    {
        public BrandService(IBaseRepository<Brand> repo) : base(repo) { }
    }
}
