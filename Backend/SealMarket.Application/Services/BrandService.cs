using SealMarket.Application.DTOs.Requests.CreateDTOs;
using SealMarket.Application.DTOs.Requests.FilterDTOs;
using SealMarket.Application.DTOs.Requests.UpdateDTOs;
using SealMarket.Application.DTOs.Responses.CreatedDTOs;
using SealMarket.Application.DTOs.Responses.ReadDTOs.BrandDtos;
using SealMarket.Application.Interfaces;
using SealMarket.Core.Entities;
using SealMarket.Core.Interfaces;
using SealMarket.Core.Models.Filters;

namespace SealMarket.Application.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _repo;

        public BrandService(IBrandRepository repo) 
        { 
            _repo = repo;
        }

        public async Task<CreatedBrandDto> CreateBrandAsync(CreateBrandDto createBrandDto)
        {
            var brand = new Brand
            (
                createBrandDto.Name, 
                createBrandDto.LogoUrl, 
                createBrandDto.Description
            );

            await _repo.AddAsync(brand);
            await _repo.SaveChangesAsync();

            return new CreatedBrandDto
            (
                brand.Id,
                brand.Name
            );
        }

        public async Task DeleteBrandAsync(int id)
        {
            if (!await _repo.ExistsAsync(id))
                throw new KeyNotFoundException($"Brand with id {id} not found.");

            await _repo.DeleteByIdAsync(id);
            await _repo.SaveChangesAsync();
        }

        public async Task<BrandDto> GetBrandInfoAsync(int id)
        {
            var brand = await _repo.GetWithProductsAsync(id);

            if (brand is null)
                throw new KeyNotFoundException($"Brand with id {id} not found.");

            return new BrandDto
            (
                brand.Id,
                brand.Name,
                brand.LogoUrl,
                brand.Description,
                brand.Products.Count
            );
        }

        public async Task<List<BrandDto>> GetBrandsAsync(BrandsFilterDto brandsFilterDto)
        {
            var filter = new BrandsFilter
            (
                brandsFilterDto.Page,
                brandsFilterDto.Size,
                brandsFilterDto.MinAverageProductPrice,
                brandsFilterDto.MaxAverageProductPrice,
                brandsFilterDto.OrderParam,
                brandsFilterDto.ByAscending,
                brandsFilterDto.SearchText
            );

            var brands = await _repo.GetBrandsAsync(filter);

            return brands.Select(brand => new BrandDto
            (
                brand.Id,
                brand.Name,
                brand.LogoUrl,
                brand.Description,
                brand.Products.Count
            )).ToList();
        }

        public async Task UpdateBrandAsync(int id, UpdateBrandDto updateBrandDto)
        {
            var brandToUpdate = await _repo.GetByIdAsync(id);

            if (brandToUpdate is null)
                throw new KeyNotFoundException($"Brand with id {id} not found.");

            brandToUpdate.UpdateInfo
            (
                updateBrandDto.Name ?? brandToUpdate.Name,
                updateBrandDto.LogoUrl ?? brandToUpdate.LogoUrl,
                updateBrandDto.Description ?? brandToUpdate.Description
            );

            await _repo.SaveChangesAsync();
        }
    }
}