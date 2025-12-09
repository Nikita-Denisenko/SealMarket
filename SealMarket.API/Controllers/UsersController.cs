using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SealMarket.Application.DTOs.Requests.CreateDTOs;
using SealMarket.Application.DTOs.Requests.FilterDTOs;
using SealMarket.Application.DTOs.Requests.UpdateDTOs;
using SealMarket.Application.Interfaces;
using System.Threading.Tasks;

namespace SealMarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersAsync([FromQuery] UsersFilterDto filterDto)
        {
            try
            {
                var users = await _service.GetUsersAsync(filterDto);
                return Ok(users);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Operation failed.");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            var userDto = await _service.GetUserAsync(id);

            if(userDto is null)
                return NotFound("User was not found.");

            return Ok(userDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserDto createUserDto)
        {
            if (!(ModelState.IsValid))
                return BadRequest(ModelState);

            try
            {
                var createdUser = await _service.CreateUserAsync(createUserDto);
                return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id}, createdUser); 
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("User was not created.");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateUserAsync
        (
            [FromRoute] int id, 
            [FromBody] UpdateUserDto updateUserDto
        )
        {
            if (!(ModelState.IsValid))
                return BadRequest(ModelState);

            try
            {
                await _service.UpdateUserAsync(id, updateUserDto);
                return Ok("User was sucсessfuly updated.");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("User was not updated.");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUserAsync(int id) 
        {
            try
            {
                await _service.DeleteUserAsync(id);
                return NoContent();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("User was not deleted.");
            }
        }
    }
}
