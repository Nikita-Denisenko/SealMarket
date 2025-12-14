using SealMarket.Application.DTOs.Requests.CreateDTOs;
using SealMarket.Application.DTOs.Requests.FilterDTOs;
using SealMarket.Application.DTOs.Requests.UpdateDTOs;
using SealMarket.Application.DTOs.Responses.CreatedDTOs;
using SealMarket.Application.DTOs.Responses.ReadDTOs.CartDtos;
using SealMarket.Application.Interfaces;
using SealMarket.Core.Interfaces;

namespace SealMarket.Application.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _repo;

        public CartService(ICartRepository repo) 
        {
            _repo = repo;
        }

        public Task<CreatedCartDto> CreateCartAsync(CreateCartDto createCartDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCartAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CartDto> GetCartAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ShortCartDto>> GetCartsAsync(CartsFilterDto cartsFilterDto)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCartAsync(int id, UpdateCartDto updateCartDto)
        {
            throw new NotImplementedException();
        }
    }
}
