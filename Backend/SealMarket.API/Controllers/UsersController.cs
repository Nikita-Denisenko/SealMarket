using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SealMarket.Application.DTOs.Requests.FilterDTOs;
using SealMarket.Application.DTOs.Requests.UpdateDTOs;
using SealMarket.Application.Interfaces;
using static SealMarket.Application.Constants.Roles;

namespace SealMarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly ICurrentAccountService _currentAccount;
        private readonly ILogger<UsersController> _logger;

        public UsersController
        (
            IUserService service, 
            ICurrentAccountService currentAccount, 
            ILogger<UsersController> logger
        )
        {
            _service = service;
            _currentAccount = currentAccount;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] UsersFilterDto filterDto)
        {
            try
            {
                var users = await _service.GetUsersAsync(filterDto);
                _logger.LogInformation("Users received successfully");
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [Authorize(Roles = Customer)]
        [HttpGet("my-profile")]
        public async Task<IActionResult> GetMyProfile()
        {
            if (_currentAccount.UserId is null)
            {
                _logger.LogWarning("User ID not found in token");
                return Unauthorized(new { error = "User ID not found in token" });
            }

            var userId = _currentAccount.UserId.Value;

            try
            {
                var userProfileDto = await _service.GetUserProfileAsync(userId);
                _logger.LogInformation("User profile with user ID {UserId} received successfully", userId);
                return Ok(userProfileDto);
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
        public async Task<IActionResult> GetPublicUserProfile([FromRoute] int id)
        {
            try
            {
                var profile = await _service.GetPublicUserProfileAsync(id);
                _logger.LogInformation("User profile with user ID {UserId} received successfully", id);
                return Ok(profile);
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

        [HttpPut("my-profile")]
        [Authorize(Roles = Customer)]
        public async Task<IActionResult> UpdateMyProfile
        (
            [FromBody] UpdateUserDto updateUserDto
        )
        {
            if (_currentAccount.UserId is null)
            {
                _logger.LogWarning("User ID not found in token");
                return Unauthorized(new { error = "User ID not found in token" });
            }

            var userId = _currentAccount.UserId.Value;

            try
            {
                await _service.UpdateUserAsync(userId, updateUserDto);
                _logger.LogInformation("User profile with user ID {UserId} updated successfully", userId);
                return Ok("User was sucсessfuly updated.");
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

        [HttpDelete("{id:int}")]
        [Authorize(Roles = Admin)]
        public async Task<IActionResult> DeleteUserAccountForAdmin(int id) 
        {
            try
            {
                await _service.DeleteUserProfileAsync(id);
                _logger.LogInformation("User account with user ID {UserId} deleted successfully", id);
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

        [HttpDelete("my-account")]
        [Authorize(Roles = Customer)]
        public async Task<IActionResult> DeleteMyAccount()
        {
            if (_currentAccount.UserId is null)
            {
                _logger.LogWarning("User ID not found in token");
                return Unauthorized(new { error = "User ID not found in token" });
            }

            var userId = _currentAccount.UserId.Value;

            try
            {
                await _service.DeleteUserProfileAsync(userId);
                _logger.LogInformation("User account deleted successfully for user ID {UserId}", userId);
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
