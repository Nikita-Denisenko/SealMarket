using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SealMarket.Application.DTOs.Requests.CreateDTOs;
using SealMarket.Application.DTOs.Requests.FilterDTOs;
using SealMarket.Application.DTOs.Requests.UpdateDTOs;
using SealMarket.Application.DTOs.Responses.CreatedDTOs;
using SealMarket.Application.Interfaces;
using static SealMarket.Application.Constants.Roles;

namespace SealMarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _service;

        public BrandsController(IBrandService service, ICurrentAccountService currentAccount)
        {
            _service = service;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBrandInfoAsync([FromRoute] int id)
        {
            try
            {
                var brand = await _service.GetBrandInfoAsync(id);
                return Ok(brand);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Operation failed.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetBrandsAsync
        (
            [FromQuery] BrandsFilterDto brandsFilterDto
        )
        {
            try
            {
                var brands = await _service.GetBrandsAsync(brandsFilterDto);
                return Ok(brands);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Operation failed.");
            }
        }

        [HttpPost]
        [Authorize(Roles = Admin)]
        public async Task<IActionResult> CreateBrandAsync
        (
            [FromBody] CreateBrandDto createBrandDto
        )
        {
            try
            {
                var createdBrand = await _service.CreateBrandAsync(createBrandDto);

                return CreatedAtAction
                (
                    nameof(GetBrandInfoAsync),
                    new {createdBrand.Id},
                    createdBrand
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Operation failed.");
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = Admin)]
        public async Task<IActionResult> DeleteBrandAsync([FromRoute] int id)
        {
            try
            {
                await _service.DeleteBrandAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Operation failed.");
            }
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = Admin)]
        public async Task<IActionResult> UpdateBrandAsync
        (
            [FromRoute] int id,
            [FromBody] UpdateBrandDto updateBrandDto
        )
        {
            try
            {
                await _service.UpdateBrandAsync(id, updateBrandDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Operation failed.");
            }
        }
    }
}
