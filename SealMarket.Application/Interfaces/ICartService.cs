using SealMarket.Application.DTOs.Requests.CreateDTOs;
using SealMarket.Application.DTOs.Requests.FilterDTOs;
using SealMarket.Application.DTOs.Requests.UpdateDTOs;
using SealMarket.Application.DTOs.Responses.CreatedDTOs;
using SealMarket.Application.DTOs.Responses.EntityDtos;

namespace SealMarket.Application.Interfaces
{
    public interface ICartService
    {
        public Task<List<ReadCartDto>> GetCartsAsync(CartsFilterDto cartsFilterDto);
        public Task<ReadCartDto> GetCartAsync(int id);
        public Task<CreatedCartDto> CreateCartAsync(CreateCartDto createCartDto);
        public Task UpdateCartAsync(int id, UpdateCartDto updateCartDto);
        public Task DeleteCartAsync(int id);
    }
}
