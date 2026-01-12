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
        private readonly ILogger<NotificationsController> _logger;

        public NotificationsController
        (
            INotificationService service, 
            ICurrentAccountService currentAccount, 
            ILogger<NotificationsController> logger
        )
        {
            _service = service;
            _currentAccount = currentAccount;
            _logger = logger;
        }

        [HttpGet("my-notifications")]
        [Authorize(Roles = Customer)]
        public async Task<IActionResult> GetMyNotifications([FromQuery] NotificationsFilterDto notificationsFilterDto)
        {
            if (_currentAccount.AccountId is null)
            {
                _logger.LogWarning("Account ID not found in token");
                return Unauthorized(new { error = "Account ID not found in token" });
            }

            var accountId = _currentAccount.AccountId.Value;

            try
            {
                var notifications = await _service.GetNotificationsAsync(notificationsFilterDto, accountId);
                _logger.LogInformation("Notifications received successfully for account ID {AccountId}", accountId);
                return Ok(notifications);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpGet]
        [Authorize(Roles = Admin)]
        public async Task<IActionResult> GetNotifications([FromQuery] NotificationsFilterDto notificationsFilterDto)
        {
            try
            {
                var notifications = await _service.GetNotificationsAsync(notificationsFilterDto, null);
                _logger.LogInformation("Notifications received successfully");
                return Ok(notifications);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = $"{Admin},{Customer}")]
        public async Task<IActionResult> GetNotification([FromRoute] int id)
        {
            try
            {
                var notification = await _service.GetNotificationAsync(id);

                if (_currentAccount.Role == Customer && notification.AccountId != _currentAccount.AccountId)
                {
                    _logger.LogWarning
                    (
                        "Unauthorized access attempt by account ID {AccountId} to notification ID {NotificationId}",
                        _currentAccount.AccountId, id
                    );

                    return Forbid("You are not authorized to access this notification");
                }

                _logger.LogInformation("Notification with ID {NotificationId} received successfully", id);

                return Ok(notification);
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

        [HttpPost]
        [Authorize(Roles = Admin)]
        public async Task<IActionResult> CreateNotification([FromBody] CreateNotificationDto createNotificationDto)
        {
            try
            {
                var createdNotification = await _service.CreateNotificationAsync(createNotificationDto);
                _logger.LogInformation("Notification with ID {NotificationId} created successfully", createdNotification.Id);
                return CreatedAtAction(nameof(GetNotification), new { id = createdNotification.Id }, createdNotification);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = Admin)]
        public async Task<IActionResult> DeleteNotification([FromRoute] int id)
        {
            try
            {
                await _service.DeleteNotificationAsync(id);
                _logger.LogInformation("Notification with ID {NotificationId} deleted successfully", id);
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
