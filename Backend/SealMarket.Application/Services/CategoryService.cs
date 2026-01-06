using SealMarket.Application.DTOs.Requests.CreateDTOs;
using SealMarket.Application.DTOs.Requests.FilterDTOs;
using SealMarket.Application.DTOs.Requests.UpdateDTOs;
using SealMarket.Application.DTOs.Responses.CreatedDTOs;
using SealMarket.Application.DTOs.Responses.ReadDTOs.CategoryDtos;
using SealMarket.Application.Interfaces;
using SealMarket.Core.Entities;
using SealMarket.Core.Interfaces;
using SealMarket.Core.Models.Filters;

namespace SealMarket.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;

        public CategoryService(ICategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<CreatedCategoryDto> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var category = new Category
            (
                createCategoryDto.Name,
                createCategoryDto.Description
            );

            await _repo.AddAsync(category);
            await _repo.SaveChangesAsync();

            return new CreatedCategoryDto
            (
                category.Id,
                category.Name
            );
        }

        public async Task DeleteCategoryAsync(int id)
        {
            if (!await _repo.ExistsAsync(id))
                throw new KeyNotFoundException($"Category with id {id} not found.");

            await _repo.DeleteByIdAsync(id);
            await _repo.SaveChangesAsync();
        }

        public async Task<List<ShortCategoryDto>> GetCategoriesAsync(CategoriesFilterDto filterDto)
        {
            var filter = new CategoriesFilter
            (
                filterDto.Page,
                filterDto.Size,
                filterDto.OrderParam,
                filterDto.ByAscending,
                filterDto.SearchText
            );

            var categories = await _repo.GetCategoriesAsync(filter);

            return categories.Select(c => new ShortCategoryDto
            (
                c.Id,
                c.Name,
                c.ImageUrl
            )).ToList();
        }

        public async Task<CategoryDto?> GetCategoryAsync(int id)
        {
            var category = await _repo.GetByIdAsync(id);

            if (category is null)
                throw new KeyNotFoundException($"Category with id {id} not found.");

            return new CategoryDto
            (
                id,
                category.Name,
                category.ImageUrl,
                category.Description,
                category.Products.Count
            );
        }

        public async Task UpdateCategoryAsync(int id, UpdateCategoryDto updateCategoryDto)
        {
            var categoryToUpdate = await _repo.GetByIdAsync(id);

            if (categoryToUpdate is null)
                throw new KeyNotFoundException($"Category with id {id} not found.");

            categoryToUpdate.UpdateInfo
            (
                updateCategoryDto.Name ?? categoryToUpdate.Name,
                updateCategoryDto.Description ?? categoryToUpdate.Description,
                updateCategoryDto.ImageUrl ?? categoryToUpdate.ImageUrl
            );

            await _repo.SaveChangesAsync();
        }
    }
}
