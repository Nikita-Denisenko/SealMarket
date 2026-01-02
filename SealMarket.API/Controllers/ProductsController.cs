using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SealMarket.Application.DTOs.Requests.CreateDTOs;
using SealMarket.Application.DTOs.Requests.FilterDTOs;
using SealMarket.Application.DTOs.Requests.UpdateDTOs;
using SealMarket.Application.Interfaces;
using static SealMarket.Application.Constants.Roles;

namespace SealMarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController
        (
            IProductService service,
            ILogger<ProductsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync
        (
            [FromQuery] ProductsFilterDto productsFilterDto
        )
        {
            try
            {
                var products = await _service.GetProductsAsync(productsFilterDto);

                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductInfoAsync([FromRoute] int id)
        {
            try
            {
                var product = await _service.GetProductInfoAsync(id);
                _logger.LogInformation("Product with ID {ProductId} received successfully", id);
                return Ok(product);
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
        public async Task<IActionResult> CreateProductAsync
        (
            [FromBody] CreateProductDto createProductDto
        )
        {
            try
            {
                var createdProduct = await _service.CreateProductAsync(createProductDto);

                _logger.LogInformation("Product with ID {ProductId} created successfully", createdProduct.Id);

                return CreatedAtAction
                (
                    nameof(GetProductInfoAsync),
                    new { createdProduct.Id },
                    createdProduct
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = Admin)]
        public async Task<IActionResult> UpdateProductAsync
        (
            [FromRoute] int id,
            [FromBody] UpdateProductDto updateProductDto
        )
        {
            try
            {
                await _service.UpdateProductAsync(id, updateProductDto);
                _logger.LogInformation("Product with ID {ProductId} updated successfully", id);
                return Ok("Product was successfuly updated");
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
        public async Task<IActionResult> DeleteProductAsync([FromRoute] int id)
        {
            try
            {
                await _service.DeleteProductAsync(id);
                _logger.LogInformation("Product with ID {ProductId} deleted successfully", id);
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
