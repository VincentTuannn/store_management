using Microsoft.AspNetCore.Mvc;
using store_management.DTO;
using store_management.Service;

namespace store_management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<productsDTO>>> GetAllProducts()
        {
            var products = await _service.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<productsDTO>> GetProductById(int id)
        {
            try
            {
                var product = await _service.GetProductByIdAsync(id);
                return Ok(product);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Product not found");
            }
        }

        [HttpPost]
        public async Task<ActionResult<productsDTO>> CreateProduct(productsDTO dto)
        {
            try
            {
                var product = await _service.CreateProductAsync(dto);
                return CreatedAtAction(nameof(GetProductById), new { id = product.Product_id }, product);
            }
            catch (Exception ex)  // Sửa: Catch Exception thay vì multiple types
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, productsDTO dto)
        {
            try
            {
                await _service.UpdateProductAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Product not found");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _service.DeleteProductAsync(id);
                return NoContent();
            }
            catch (Exception ex)  // Sửa: Catch Exception thay vì multiple types
            {
                return BadRequest(ex.Message);
            }
        }
    }
}