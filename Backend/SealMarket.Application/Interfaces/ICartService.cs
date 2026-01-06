using SealMarket.Application.DTOs.Requests.CreateDTOs;
using SealMarket.Application.DTOs.Requests.FilterDTOs;
using SealMarket.Application.DTOs.Responses.CreatedDTOs;
using SealMarket.Application.DTOs.Responses.ReadDTOs.CartDtos;

namespace SealMarket.Application.Interfaces
{
    public interface ICartService
    {
        public Task<List<ShortCartDto>> GetCartsForAdminAsync(CartsFilterDto cartsFilterDto);
        public Task<CartDto> GetCartForAdminAsync(int id);
        public Task<CartDto> GetMyCartAsync(int accountId);
        public Task<CreatedCartItemDto> AddItemToMyCartAsync(int accountId, CreateCartItemDto createCartItemDto);
        public Task RemoveItemFromMyCartAsync(int accountId, int itemId, bool removeAll = true);
    }
}
