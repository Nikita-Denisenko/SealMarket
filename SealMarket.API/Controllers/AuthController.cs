using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using SealMarket.Application.DTOs.Requests.AuthDTOs;
using SealMarket.Application.Interfaces;

namespace SealMarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService service, ILogger<AuthController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto registerDto)
        {
            _logger.LogInformation("Registration attempt. Email: {Email}", registerDto.Email);

            try
            {
                var createdAccount = await _service.RegisterAsync(registerDto);

                if (createdAccount is null)
                    return BadRequest(new { error = "Registration failed." });

                _logger.LogInformation(
                    "User registered successfully. AccountId: {AccountId}, Email: {Email}",
                    createdAccount.AccountId, createdAccount.Email);

                return CreatedAtAction(nameof(RegisterAsync), createdAccount);
            }
            catch (InvalidOperationException ex) 
            {
                _logger.LogWarning(ex, "Registration conflict");
                return Conflict(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto loginDto)
        {
            _logger.LogInformation("Login attempt. Login: {Credentials}", loginDto.Login);

            try
            {
                var authResult = await _service.LoginAsync(loginDto);

                if (authResult is null)
                    return Unauthorized(new { error = "Invalid credentials." });

                _logger.LogInformation(
                    "User logged in successfully. AccountId: {AccountId}, TokenGenerated: {HasToken}",
                     authResult.AccountId,
                    !string.IsNullOrEmpty(authResult.Token));

                return Ok(authResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
