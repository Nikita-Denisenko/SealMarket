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

        public ProductsController(IProductService service, ICurrentAccountService currentAccount)
        {
            _service = service;
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

                Console.WriteLine(ex);
                return BadRequest("Operation failed");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductInfoAsync([FromRoute] int id)
        {
            try
            {
                var product = await _service.GetProductInfoAsync(id);

                if (product is null)
                    return NotFound($"Product with ID {id} was not found.");

                return Ok(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest("Operation failed");
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

                if (createdProduct is null)
                    return BadRequest("Creation failed.");

                return CreatedAtAction
                (
                    nameof(GetProductInfoAsync),
                    new { createdProduct.Id },
                    createdProduct
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Operation failed");
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
                return Ok("Product was successfuly updated");
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Operation failed");
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = Admin)]
        public async Task<IActionResult> DeleteProductAsync([FromRoute] int id)
        {
            try
            {
                await _service.DeleteProductAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Product was not deleted.");
            }
        }
    }
}
