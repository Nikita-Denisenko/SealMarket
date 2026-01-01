using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SealMarket.Application.DTOs.Requests.CreateDTOs;
using SealMarket.Application.DTOs.Requests.FilterDTOs;
using SealMarket.Application.Interfaces;
using static SealMarket.Application.Constants.Roles;

namespace SealMarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _service;
        private readonly ICurrentAccountService _currentAccount;

        public CartsController(ICartService service, ICurrentAccountService currentAccount)
        {
            _service = service;
            _currentAccount = currentAccount;
        }

        [HttpGet("my-cart")]
        [Authorize(Roles = Customer)]
        public async Task<IActionResult> GetMyCartAsync()
        {
            if (_currentAccount.AccountId is null)
                return Unauthorized("Account ID not found in token");
            var accountId = _currentAccount.AccountId.Value;
            try
            {
                var cart = await _service.GetMyCartAsync(accountId);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Operation failed.");
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = Admin)]
        public async Task<IActionResult> GetCartForAdminAsync([FromRoute] int id)
        {
            try
            {
                var cart = await _service.GetCartForAdminAsync(id);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Operation failed.");
            }
        }

        [HttpGet]
        [Authorize(Roles = Admin)]
        public async Task<IActionResult> GetCartsForAdminAsync
        (
            [FromQuery] CartsFilterDto cartsFilterDto
        )
        {
            try
            {
                var carts = await _service.GetCartsForAdminAsync(cartsFilterDto);
                return Ok(carts);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Operation failed.");
            }
        }

        [HttpPost("my-cart/add-item")]
        [Authorize(Roles = Customer)]
        public async Task<IActionResult> AddItemToMyCartAsync
        (
            [FromBody] CreateCartItemDto createCartItemDto
        )
        {
            if (_currentAccount.AccountId is null)
                return Unauthorized("Account ID not found in token");

            try
            {
                var createdCartItem = await _service.AddItemToMyCartAsync(_currentAccount.AccountId.Value, createCartItemDto);
                return CreatedAtAction(nameof(GetMyCartAsync), new { createdCartItem.Id }, createdCartItem);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Operation failed.");
            }
        }

        [HttpDelete("my-cart/remove-item/{itemId:int}")]
        [Authorize(Roles = Customer)]
        public async Task<IActionResult> RemoveItemFromMyCartAsync
        (
            [FromRoute] int itemId,
            [FromQuery] bool removeAll = true
        )
        {
            if (_currentAccount.AccountId is null)
                return Unauthorized("Account ID not found in token");
            try
            {
                await _service.RemoveItemFromMyCartAsync(_currentAccount.AccountId.Value, itemId, removeAll);
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Operation failed.");
            }
        }
    }
}
