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
        private readonly ILogger<BrandsController> _logger;

        public BrandsController(IBrandService service, ILogger<BrandsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBrandInfoAsync([FromRoute] int id)
        {
            try
            {
                var brand = await _service.GetBrandInfoAsync(id);
                _logger.LogInformation("Brand with brand ID {BrandId} received successfully", id);
                return Ok(brand);
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

        [HttpGet]
        public async Task<IActionResult> GetBrandsAsync
        (
            [FromQuery] BrandsFilterDto brandsFilterDto
        )
        {
            try
            {
                var brands = await _service.GetBrandsAsync(brandsFilterDto);
                _logger.LogInformation("Brands received successfully");
                return Ok(brands);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error");
                return StatusCode(500, new { error = "Internal server error" });
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
                _logger.LogInformation("Brand with brand ID {BrandId} created successfully", createdBrand.Id);

                return CreatedAtAction
                (
                    nameof(GetBrandInfoAsync),
                    new {createdBrand.Id},
                    createdBrand
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = Admin)]
        public async Task<IActionResult> DeleteBrandAsync([FromRoute] int id)
        {
            try
            {
                await _service.DeleteBrandAsync(id);
                _logger.LogInformation("Brand with brand ID {BrandId} deleted successfully", id);
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
                _logger.LogInformation("Brand with brand ID {BrandId} updated successfully", id);
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
