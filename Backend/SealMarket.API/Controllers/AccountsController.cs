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
        private readonly ILogger<AccountsController> _logger;

        public AccountsController
        (
            IAccountService service, 
            ICurrentAccountService currentAccount,
            ILogger<AccountsController> logger
        )
        {
            _service = service;
            _currentAccount = currentAccount;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = Admin)]
        public async Task<IActionResult> GetAccountsAsync([FromQuery] AccountsFilterDto accountsFilterDto)
        {
            try
            {
                var accounts = await _service.GetAccountsAsync(accountsFilterDto);
                _logger.LogInformation("Accounts received successfully for Admin");
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpGet("my-account")]
        [Authorize(Roles = Customer)]
        public async Task<IActionResult> GetMyAccountAsync()
        {
            if (_currentAccount.AccountId is null)
            {
                _logger.LogWarning("Account ID not found in token");
                return Unauthorized(new { error = "Account ID not found in token" });
            }

            var accountId = _currentAccount.AccountId.Value;

            try
            {
                var account = await _service.GetAccountAsync(accountId);
                _logger.LogInformation("Account with account ID {AccountId} received successfully", accountId);
                return Ok(account);
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

        [HttpGet("{id:int}")]
        [Authorize(Roles = Admin)]
        public async Task<IActionResult> GetAccountForAdminAsync([FromRoute] int id)
        {
            try
            {
                var account = await _service.GetAccountAsync(id);
                _logger.LogInformation("Account with account ID {AccountId} received successfully", id);
                return Ok(account);
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

        [HttpPut("my-account")]
        [Authorize(Roles = Customer)]
        public async Task<IActionResult> UpdateMyAccountAsync
        (
            [FromBody] UpdateAccountDto updateAccountDto
        )
        {
            if (_currentAccount.AccountId is null)
            {
                _logger.LogWarning("Account ID not found in token");
                return Unauthorized(new { error = "Account ID not found in token" });
            }

            var accountId = _currentAccount.AccountId.Value;

            try
            {
                await _service.UpdateAccountAsync(accountId, updateAccountDto);
                _logger.LogInformation("Account with account ID {AccountId} updated successfully", accountId);
                return Ok("Account was successfuly updated.");
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
