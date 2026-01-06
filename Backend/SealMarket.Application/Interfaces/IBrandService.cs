using SealMarket.Application.DTOs.Requests.CreateDTOs;
using SealMarket.Application.DTOs.Requests.FilterDTOs;
using SealMarket.Application.DTOs.Requests.UpdateDTOs;
using SealMarket.Application.DTOs.Responses.CreatedDTOs;
using SealMarket.Application.DTOs.Responses.ReadDTOs.BrandDtos;

namespace SealMarket.Application.Interfaces
{
    public interface IBrandService
    {
        public Task<List<BrandDto>> GetBrandsAsync(BrandsFilterDto brandsFilterDto);
         public Task<BrandDto> GetBrandInfoAsync(int id);
        public Task<CreatedBrandDto> CreateBrandAsync(CreateBrandDto createBrandDto);
        public Task UpdateBrandAsync(int id, UpdateBrandDto updateBrandDto);
        public Task DeleteBrandAsync(int id);
    }
}
