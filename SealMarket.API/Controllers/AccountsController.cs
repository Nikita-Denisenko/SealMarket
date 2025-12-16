using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SealMarket.Application.DTOs.Requests.FilterDTOs;
using SealMarket.Application.DTOs.Requests.UpdateDTOs;
using SealMarket.Application.Interfaces;

namespace SealMarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _service;

        public AccountsController(IAccountService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAccountsAsync([FromQuery] AccountsFilterDto accountsFilterDto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var accounts = await _service.GetAccountsAsync(accountsFilterDto);
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Operation failed.");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAccountAsync([FromRoute] int id)
        {
            try
            {
                var account = await _service.GetAccountAsync(id);

                if (account is null)
                    return NotFound();

                return Ok(account);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Operation failed.");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAccountAsync
        (
            [FromRoute] int id,
            [FromBody] UpdateAccountDto updateAccountDto
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _service.UpdateAccountAsync(id, updateAccountDto);
                return Ok("Account was successfuly updated.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Operation failed.");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAccountAsync([FromRoute] int id)
        {
            try
            {
                await _service.DeleteAccountAsync(id);
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
