using SealMarket.Application.DTOs.Requests.FilterDTOs;
using SealMarket.Application.DTOs.Responses.ReadDTOs;
using SealMarket.Application.DTOs.Responses.ReadDTOs.CartDtos;
using SealMarket.Application.Interfaces;
using SealMarket.Core.Interfaces;
using SealMarket.Core.Models.Filters;

namespace SealMarket.Application.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _repo;

        public CartService(ICartRepository repo) 
        {
            _repo = repo;
        }

        public async Task<CartDto> GetMyCartAsync(int accountId)
        {
            var cart = await _repo.GetCartByAccountAsync(accountId);

            if (cart is null)
                throw new KeyNotFoundException("Cart was not found!");

            return new CartDto
            (
                cart.Id,
                cart.Name,
                cart.TotalPrice,
                cart.CartItems.Select(
                    ci => new CartItemDto
                    (
                        ci.Id,
                        ci.ProductId,
                        ci.Quantity,
                        ci.AddedAt,
                        ci.Product.Name,
                        ci.Product.ImageUrl,
                        ci.Product.Price,
                        ci.TotalPrice
                    )
                )
                .ToList()
            );
        }

        public async Task<CartDto> GetCartForAdminAsync(int id)
        {
            var cart = await _repo.GetCartWithIncludesAsync(id);

            if (cart is null)
                throw new KeyNotFoundException("Cart was not found!");

            return new CartDto
            (
                cart.Id,
                cart.Name,
                cart.TotalPrice,
                cart.CartItems.Select(
                    ci => new CartItemDto
                    (
                        ci.Id,
                        ci.ProductId,
                        ci.Quantity,
                        ci.AddedAt,
                        ci.Product.Name,
                        ci.Product.ImageUrl,
                        ci.Product.Price,
                        ci.TotalPrice
                    )
                )
                .ToList()
            );
        }

        public async Task<List<ShortCartDto>> GetCartsForAdminAsync(CartsFilterDto cartsFilterDto)
        {
            if (cartsFilterDto is null)
                throw new ArgumentNullException(nameof(cartsFilterDto));

            var filter = new CartsFilter
            (
                cartsFilterDto.Page,
                cartsFilterDto.Size,
                cartsFilterDto.MinTotalPrice,
                cartsFilterDto.MaxTotalPrice,
                cartsFilterDto.OrderParam,
                cartsFilterDto.ByAscending,
                cartsFilterDto.SearchText
            );

            var carts = await _repo.GetCartsAsync(filter);

            var shortCartDtos = carts
                .Select(c => new ShortCartDto(c.Id, c.AccountId))
                .ToList();

            return shortCartDtos;
        }
    }
}
