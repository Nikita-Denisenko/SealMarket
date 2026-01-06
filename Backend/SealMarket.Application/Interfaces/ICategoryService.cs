using SealMarket.Application.DTOs.Requests.CreateDTOs;
using SealMarket.Application.DTOs.Requests.FilterDTOs;
using SealMarket.Application.DTOs.Requests.UpdateDTOs;
using SealMarket.Application.DTOs.Responses.CreatedDTOs;
using SealMarket.Application.DTOs.Responses.ReadDTOs.CategoryDtos;

namespace SealMarket.Application.Interfaces
{
    public interface ICategoryService
    {
        public Task<List<ShortCategoryDto>> GetCategoriesAsync(CategoriesFilterDto filterDto);
        public Task<CategoryDto?> GetCategoryAsync(int id);
        public Task<CreatedCategoryDto> CreateCategory(CreateCategoryDto createCategoryDto);
        public Task UpdateCategoryAsync(int id, UpdateCategoryDto updateCategoryDto);
        public Task DeleteCategoryAsync(int id);
    }
}
