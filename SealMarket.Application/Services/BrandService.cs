using SealMarket.Application.DTOs.Requests.CreateDTOs;
using SealMarket.Application.DTOs.Requests.FilterDTOs;
using SealMarket.Application.DTOs.Requests.UpdateDTOs;
using SealMarket.Application.DTOs.Responses.CreatedDTOs;
using SealMarket.Application.DTOs.Responses.ReadDTOs.BrandDtos;
using SealMarket.Application.Interfaces;
using SealMarket.Core.Interfaces;

namespace SealMarket.Application.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _repo;

        public BrandService(IBrandRepository repo) 
        { 
            _repo = repo;
        }

        public Task<CreatedBrandDto> CreateBrandAsync(CreateBrandDto createBrandDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteBrandAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BrandDto> GetBrandInfoAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<BrandDto>> GetBrandsAsync(BrandsFilterDto brandsFilterDto)
        {
            throw new NotImplementedException();
        }

        public Task UpdateBrandAsync(int id, UpdateBrandDto updateBrandDto)
        {
            throw new NotImplementedException();
        }
    }
}
