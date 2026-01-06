using SealMarket.Application.DTOs.Requests.CreateDTOs;
using SealMarket.Application.DTOs.Requests.FilterDTOs;
using SealMarket.Application.DTOs.Requests.UpdateDTOs;
using SealMarket.Application.DTOs.Responses.CreatedDTOs;
using SealMarket.Application.DTOs.Responses.ReadDTOs.ProductDtos;
using SealMarket.Application.Interfaces;
using SealMarket.Core.Entities;
using SealMarket.Core.Interfaces;
using SealMarket.Core.Models.Filters;

namespace SealMarket.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        private readonly IBrandRepository _brandRepo;

        public ProductService(IProductRepository repo, IBrandRepository brandRepo) 
        { 
            _repo = repo;
            _brandRepo = brandRepo;
        }

        public async Task<CreatedProductDto> CreateProductAsync(CreateProductDto createProductDto)
        {
            if (!await _brandRepo.ExistsAsync(createProductDto.BrandId))
                throw new KeyNotFoundException($"Brand with id {createProductDto.BrandId} not found");

            var product = new Product
            (
                createProductDto.Name,
                createProductDto.BrandId,
                createProductDto.CategoryId,
                createProductDto.Description,
                createProductDto.ImageUrl,
                createProductDto.Quantity,
                createProductDto.Price,
                createProductDto.IsActive
            );

            await _repo.AddAsync(product);

            await _repo.SaveChangesAsync();

            return new CreatedProductDto
            (
                product.Id,
                product.BrandId,
                product.Name
            );
        }

        public async Task DeleteProductAsync(int id)
        {
            if (!await _repo.ExistsAsync(id))
                throw new KeyNotFoundException("Product to delete was not found");

            await _repo.DeleteByIdAsync(id);

            await _repo.SaveChangesAsync();
        }

        public async Task<ProductDto> GetProductInfoAsync(int id)
        {
            var product = await _repo.GetWithBrandByIdAsync(id);

            if (product is null)
                throw new KeyNotFoundException("Product was not found");

            return new ProductDto
            (
                product.Id,
                product.Name,
                product.Description,
                product.ImageUrl,
                product.Quantity,
                product.Price,
                product.CreatedAt,
                product.IsActive,
                product.BrandId,
                product.Brand.Name,
                product.CategoryId,
                product.Category.Name
            );
        }

        public async Task<List<ShortProductDto>> GetProductsAsync(ProductsFilterDto filterDto)
        {
            var filter = new ProductsFilter
            (
                filterDto.Page,
                filterDto.Size,
                filterDto.MinPrice,
                filterDto.MaxPrice,
                filterDto.OrderParam,
                filterDto.ByAscending,
                filterDto.SearchText,
                filterDto.CategoryName
            );

            var products = await _repo.GetProductsAsync(filter);

            var productShortDtos = products
                .Select(p =>
                    new ShortProductDto
                    (
                        p.Id,
                        p.Name,
                        p.ImageUrl,
                        p.Price,
                        p.BrandId,
                        p.CategoryId
                    )
                ).ToList();

            return productShortDtos;
        }

        public async Task UpdateProductAsync(int id, UpdateProductDto updateProductDto)
        {
            var productToUpdate = await _repo.GetByIdAsync(id);

            if (productToUpdate is null)
                throw new KeyNotFoundException("Product to update was not found");

            productToUpdate.UpdateInfo
            (
                updateProductDto.Name ?? productToUpdate.Name,
                updateProductDto.Description ?? productToUpdate.Description,
                updateProductDto.ImageUrl ?? productToUpdate.ImageUrl,
                updateProductDto.Quantity ?? productToUpdate.Quantity,
                updateProductDto.Price ?? productToUpdate.Price,
                updateProductDto.IsActive ?? productToUpdate.IsActive
            );

            await _repo.SaveChangesAsync();
        }
    }
}
