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

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto registerDto)
        {
            try
            {
                var createdAccount = await _service.RegisterAsync(registerDto);

                if (createdAccount == null)
                    return BadRequest("Registration failed.");

                return CreatedAtAction(nameof(RegisterAsync), createdAccount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Operation failed");
            }
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto loginDto)
        {
            try
            {
                var authResult = await _service.LoginAsync(loginDto);

                if (authResult == null)
                    return Unauthorized("Invalid credentials.");

                return Ok(authResult);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Operation failed");
            }
        }
    }
}
