using SealMarket.Application.DTOs.Requests.CreateDTOs;
using SealMarket.Application.DTOs.Requests.FilterDTOs;
using SealMarket.Application.DTOs.Responses.CreatedDTOs;
using SealMarket.Application.DTOs.Responses.ReadDTOs;
using SealMarket.Application.DTOs.Responses.ReadDTOs.CartDtos;
using SealMarket.Application.Interfaces;
using SealMarket.Core.Entities;
using SealMarket.Core.Interfaces;
using SealMarket.Core.Models.Filters;

namespace SealMarket.Application.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _repo;
        private readonly IProductRepository _productRepo;

        public CartService(ICartRepository repo, IProductRepository productRepo) 
        {
            _repo = repo;
            _productRepo = productRepo;
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

        public async Task<CreatedCartItemDto> AddItemToMyCartAsync(int accountId, CreateCartItemDto createCartItemDto)
        {
            var cart = await _repo.GetCartByAccountAsync(accountId);
            var productId = createCartItemDto.ProductId;

            if (cart is null)
                throw new KeyNotFoundException("Cart was not found!");

            if (!await _productRepo.ExistsAsync(productId))
                throw new KeyNotFoundException("Product was not found!");

            var item = new CartItem(productId, cart.Id, createCartItemDto.Quantity);

            cart.AddItem(item);

            await _repo.SaveChangesAsync();

            return new CreatedCartItemDto
            (
                item.Id,
                item.Product.Name,
                item.ProductId,
                item.CartId
            );
        }

        public async Task RemoveItemFromMyCartAsync(int accountId, int itemId, bool removeAll = true)
        {
            var cart = await _repo.GetCartByAccountAsync(accountId);

            if (cart is null)
                throw new KeyNotFoundException("Cart was not found!");

            var item = cart.CartItems.FirstOrDefault(ci => ci.Id == itemId);

            if (item is null)
                throw new KeyNotFoundException("Cart item was not found!");

            cart.RemoveItem(item, removeAll);
            await _repo.SaveChangesAsync();
        }
    }
}
