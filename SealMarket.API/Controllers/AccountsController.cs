using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SealMarket.Application.DTOs.Requests.FilterDTOs;
using SealMarket.Application.DTOs.Requests.UpdateDTOs;
using SealMarket.Application.Interfaces;
using static SealMarket.Application.Constants.Roles;

namespace SealMarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _service;
        private readonly ICurrentAccountService _currentAccount;

        public AccountsController(IAccountService service, ICurrentAccountService currentAccount)
        {
            _service = service;
            _currentAccount = currentAccount;
        }

        [HttpGet]
        [Authorize(Roles = Admin)]
        public async Task<IActionResult> GetAccountsAsync([FromQuery] AccountsFilterDto accountsFilterDto)
        {
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

        [HttpGet("my-account")]
        [Authorize(Roles = Customer)]
        public async Task<IActionResult> GetMyAccountAsync()
        {
            if (_currentAccount.AccountId is null)
                return Unauthorized("Account ID not found in token");

            var accountId = _currentAccount.AccountId.Value;

            try
            {
                var account = await _service.GetAccountAsync(accountId);

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

        [HttpGet("{id:int}")]
        [Authorize(Roles = Admin)]
        public async Task<IActionResult> GetAccountForAdminAsync([FromRoute] int id)
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

        [HttpPut]
        [Authorize(Roles = Customer)]
        public async Task<IActionResult> UpdateMyAccountAsync
        (
            [FromBody] UpdateAccountDto updateAccountDto
        )
        {
            if (_currentAccount.AccountId is null)
                return Unauthorized("Account ID not found in token");

            var accountId = _currentAccount.AccountId.Value;

            try
            {
                await _service.UpdateAccountAsync(accountId, updateAccountDto);
                return Ok("Account was successfuly updated.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Operation failed.");
            }
        }
    }
}
