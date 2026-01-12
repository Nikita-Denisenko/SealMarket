using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SealMarket.Application.DTOs.Requests.CreateDTOs;
using SealMarket.Application.DTOs.Requests.FilterDTOs;
using SealMarket.Application.Interfaces;
using static SealMarket.Application.Constants.Roles;
using Microsoft.Extensions.Logging;

namespace SealMarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _service;
        private readonly ICurrentAccountService _currentAccount;
        private readonly ILogger<CartsController> _logger;

        public CartsController
        (
            ICartService service, 
            ICurrentAccountService currentAccount, 
            ILogger<CartsController> logger
        )
        {
            _service = service;
            _currentAccount = currentAccount;
            _logger = logger;
        }

        [HttpGet("my-cart")]
        [Authorize(Roles = Customer)]
        public async Task<IActionResult> GetMyCart()
        {
            if (_currentAccount.AccountId is null) 
            {
                _logger.LogWarning("Account ID not found in token");
                return Unauthorized(new { error = "Account ID not found in token" });
            }
      
            var accountId = _currentAccount.AccountId.Value;
            try
            {
                var cart = await _service.GetMyCartAsync(accountId);
                _logger.LogInformation("Cart received successfully for account ID {AccountId}", accountId);
                return Ok(cart);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, ex.Message);
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = Admin)]
        public async Task<IActionResult> GetCartForAdmin([FromRoute] int id)
        {
            try
            {
                var cart = await _service.GetCartForAdminAsync(id);
                _logger.LogInformation("Cart with ID {CartId} received successfully", id);
                return Ok(cart);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, ex.Message);
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpGet]
        [Authorize(Roles = Admin)]
        public async Task<IActionResult> GetCartsForAdmin
        (
            [FromQuery] CartsFilterDto cartsFilterDto
        )
        {
            try
            {
                var carts = await _service.GetCartsForAdminAsync(cartsFilterDto);
                _logger.LogInformation("Carts received successfully with provided filters");
                return Ok(carts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpPost("my-cart/add-item")]
        [Authorize(Roles = Customer)]
        public async Task<IActionResult> AddItemToMyCart
        (
            [FromBody] CreateCartItemDto createCartItemDto
        )
        {
            if (_currentAccount.AccountId is null)
            {
                _logger.LogWarning("Account ID not found in token");    
                return Unauthorized(new { error = "Account ID not found in token" });
            }

            try
            {
                var createdCartItem = await _service.AddItemToMyCartAsync(_currentAccount.AccountId.Value, createCartItemDto);
                _logger.LogInformation("Item added to cart successfully for account ID {AccountId}", _currentAccount.AccountId.Value);
                return CreatedAtAction(nameof(GetMyCart), new { createdCartItem.Id }, createdCartItem);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, ex.Message);
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpDelete("my-cart/remove-item/{itemId:int}")]
        [Authorize(Roles = Customer)]
        public async Task<IActionResult> RemoveItemFromMyCart
        (
            [FromRoute] int itemId,
            [FromQuery] bool removeAll = true
        )
        {
            var accountId = _currentAccount.AccountId;

            if (accountId is null)
            {
                _logger.LogWarning("Account ID not found in token");
                return Unauthorized("Account ID not found in token");
            }
               
            try
            {
                await _service.RemoveItemFromMyCartAsync(accountId.Value, itemId, removeAll);
                _logger.LogInformation(
                    "Item with ID {ItemId} removed from cart for account ID {AccountId}", itemId, accountId.Value
                );
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, ex.Message);
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }
    }
}