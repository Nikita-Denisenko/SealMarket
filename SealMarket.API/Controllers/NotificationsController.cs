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
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _service;
        private readonly ICurrentAccountService _currentAccount;

        public NotificationsController(INotificationService service, ICurrentAccountService currentAccount)
        {
            _service = service;
            _currentAccount = currentAccount;
        }

        [HttpGet("my-notifications")]
        [Authorize(Roles = Customer)]
        public async Task<IActionResult> GetMyNotificationsAsync([FromQuery] NotificationsFilterDto notificationsFilterDto)
        {
            if (_currentAccount.UserId is null)
                return Unauthorized("User ID not found in token");

            var accountId = _currentAccount.UserId.Value;

            try
            {
                var notifications = await _service.GetNotificationsAsync(notificationsFilterDto, accountId);
                return Ok(notifications);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Operation failed");
            }
        }

        [HttpGet]
        [Authorize(Roles = Admin)]
        public async Task<IActionResult> GetNotificationsAsync([FromQuery] NotificationsFilterDto notificationsFilterDto)
        {
            try
            {
                var notifications = await _service.GetNotificationsAsync(notificationsFilterDto, null);
                return Ok(notifications);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Operation failed");
            }
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = $"{Admin},{Customer}")]
        public async Task<IActionResult> GetNotificationAsync([FromRoute] int id)
        {
            try
            {
                var notification = await _service.GetNotificationAsync(id);

                if (_currentAccount.Role == Customer && notification.AccountId != _currentAccount.AccountId)
                    return Forbid("You are not authorized to access this notification");

                return Ok(notification);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Operation failed");
            }
        }

        [HttpPost]
        [Authorize(Roles = Admin)]
        public async Task<IActionResult> CreateNotificationAsync([FromBody] CreateNotificationDto createNotificationDto)
        {
            try
            {
                var createdNotification = await _service.CreateNotificationAsync(createNotificationDto);
                return CreatedAtAction(nameof(GetNotificationAsync), new { id = createdNotification.Id }, createdNotification);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Operation failed");
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = Admin)]
        public async Task<IActionResult> DeleteNotificationAsync([FromRoute] int id)
        {
            try
            {
                await _service.DeleteNotificationAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Operation failed");
            }
        }
    }
}
