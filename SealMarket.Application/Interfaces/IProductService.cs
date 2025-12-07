using SealMarket.Application.DTOs.Requests.CreateDTOs;
using SealMarket.Application.DTOs.Requests.FilterDTOs;
using SealMarket.Application.DTOs.Requests.UpdateDTOs;
using SealMarket.Application.DTOs.Responses.CreatedDTOs;
using SealMarket.Application.DTOs.Responses.EntityDtos;

namespace SealMarket.Application.Interfaces
{
    public interface IProductService
    {
        public Task<List<ReadProductDto>> GetAllProductsAsync();
        public Task<List<ReadProductDto>> GetProductsAsync(ProductsFilterDto filter);
        public Task<ReadProductDto> GetProductAsync(int id);
        public Task<CreatedProductDto> CreateProductAsync(CreateProductDto createProductDto);
        public Task UpdateProductAsync(int id, UpdateProductDto updateProductDto);
        public Task DeleteProductAsync(int id);
    }
}
