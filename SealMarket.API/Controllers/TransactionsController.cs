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
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _service;
        private readonly ICurrentAccountService _currentAccount;
        private readonly ILogger<TransactionsController> _logger;

        public TransactionsController
        (
            ITransactionService service, 
            ICurrentAccountService currentAccount,
            ILogger<TransactionsController> logger
        )
        {
            _service = service;
            _currentAccount = currentAccount;
            _logger = logger;
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = $"{Admin},{Customer}")]
        public async Task<IActionResult> GetTransactionAsync([FromRoute] int id)
        {
            try
            {
                var transaction = await _service.GetTransactionAsync(id);
                
                if (_currentAccount.Role == Customer
                    && transaction.AccountId != _currentAccount.AccountId)
                {
                    _logger.LogWarning
                    (
                        "Unauthorized access attempt to transaction ID {TransactionId} by Account ID {AccountId}.", 
                        id, _currentAccount.AccountId
                    );

                    return Forbid("You are not authorized to access this transaction");
                }

                _logger.LogInformation("Transaction with ID {TransactionId} retrieved successfully.", id);
                return Ok(transaction);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning("Transaction with ID {TransactionId} not found: {ErrorMessage}", id, ex.Message);
                return NotFound(new {error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpGet("my-transactions")]
        [Authorize(Roles = $"{Admin},{Customer}")]
        public async Task<IActionResult> GetTransactionsAsync
        (
            [FromQuery] TransactionsFilterDto filterDto
        )
        {
            if (_currentAccount.AccountId is null)
            {
                _logger.LogWarning("Account ID not found in token");
                return Unauthorized(new { error = "Account ID not found in token" });
            }

            try
            {
                var transactions = await _service.GetTransactionsAsync(filterDto, _currentAccount.AccountId.Value);
                _logger.LogInformation("Transactions retrieved successfully for Account ID {AccountId}.", _currentAccount.AccountId.Value);
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpPost]
        [Authorize(Roles = Admin)]
        public async Task<IActionResult> CreateTransactionAsync
        (
            [FromBody] CreateTransactionDto createTransactionDto
        )
        {
            try
            {
                var createdTransaction = await _service.CreateTransactionAsync(createTransactionDto);
                _logger.LogInformation("Transaction created successfully with ID {TransactionId}.", createdTransaction.Id);
                return CreatedAtAction(nameof(GetTransactionAsync), new { id = createdTransaction.Id }, createdTransaction);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }
    }
}