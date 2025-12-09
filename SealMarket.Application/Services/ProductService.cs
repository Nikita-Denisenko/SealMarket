using SealMarket.Application.DTOs.Requests.CreateDTOs;
using SealMarket.Application.DTOs.Requests.FilterDTOs;
using SealMarket.Application.DTOs.Requests.UpdateDTOs;
using SealMarket.Application.DTOs.Responses.CreatedDTOs;
using SealMarket.Application.DTOs.Responses.EntityDtos;
using SealMarket.Application.Interfaces;
using SealMarket.Core.Interfaces;

namespace SealMarket.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;

        public ProductService(IProductRepository repo) 
        { 
            _repo = repo;
        }

        public Task<CreatedProductDto> CreateProductAsync(CreateProductDto createProductDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ReadProductDto> GetProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ReadProductDto>> GetProductsAsync(ProductsFilterDto filter)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProductAsync(int id, UpdateProductDto updateProductDto)
        {
            throw new NotImplementedException();
        }
    }
}
