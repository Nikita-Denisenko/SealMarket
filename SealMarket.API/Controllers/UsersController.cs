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

        public UsersController(IUserService service, ICurrentAccountService currentAccount)
        {
            _service = service;
            _currentAccount = currentAccount;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersAsync([FromQuery] UsersFilterDto filterDto)
        {
            try
            {
                var users = await _service.GetUsersAsync(filterDto);
                return Ok(users);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Operation failed.");
            }
        }

        [Authorize(Roles = Customer)]
        [HttpGet("my-profile")]
        public async Task<IActionResult> GetMyProfileAsync()
        {
            if (_currentAccount.UserId is null)
                return Unauthorized("User ID not found in token");

            var userId = _currentAccount.UserId.Value;

            try
            {
                var userProfileDto = await _service.GetUserProfileAsync(userId);
                return Ok(userProfileDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Operation failed");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPublicUserProfileAsync([FromRoute] int id)
        {
            try
            {
                var profile = await _service.GetPublicUserProfileAsync(id);
                return Ok(profile);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Operation failed");
            }
        }

        [HttpPut("my-profile")]
        [Authorize(Roles = Customer)]
        public async Task<IActionResult> UpdateMyProfileAsync
        (
            [FromBody] UpdateUserDto updateUserDto
        )
        {
            if (_currentAccount.UserId is null)
                return Unauthorized("User ID not found in token");

            var userId = _currentAccount.UserId.Value;

            try
            {
                await _service.UpdateUserAsync(userId, updateUserDto);
                return Ok("User was sucсessfuly updated.");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("User was not updated.");
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = Admin)]
        public async Task<IActionResult> DeleteUserAccountForAdminAsync(int id) 
        {
            try
            {
                await _service.DeleteUserProfileAsync(id);
                return NoContent();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Account was not deleted.");
            }
        }

        [HttpDelete("my-account")]
        [Authorize(Roles = Customer)]
        public async Task<IActionResult> DeleteMyAccountAsync()
        {
            if (_currentAccount.UserId is null)
                return Unauthorized("User ID not found in token");

            var userId = _currentAccount.UserId.Value;

            try
            {
                await _service.DeleteUserProfileAsync(userId);
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Account was not deleted.");
            }
        }
    }
}
