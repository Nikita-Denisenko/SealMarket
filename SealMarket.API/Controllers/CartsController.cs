using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

                if (cart is null)
                    return NotFound($"Cart with AccountID {accountId} not found.");

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

                if (cart is null)
                    return NotFound($"Cart with ID {id} not found.");

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
    }
}
