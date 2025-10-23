using Microsoft.AspNetCore.Mvc;
using store_management.DTO;
using store_management.Service;

namespace store_management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<categoriesDTO>>> GetAllCategories()
        {
            var categories = await _service.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<categoriesDTO>> GetCategoryById(int id)
        {
            try
            {
                var category = await _service.GetCategoryByIdAsync(id);
                return Ok(category);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Category not found");
            }
        }

        [HttpPost]
        public async Task<ActionResult<categoriesDTO>> CreateCategory(categoriesDTO dto)
        {
            try
            {
                var category = await _service.CreateCategoryAsync(dto);
                return CreatedAtAction(nameof(GetCategoryById), new { id = category.Category_id }, category);
            }
            catch (Exception ex)  
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, categoriesDTO dto)
        {
            try
            {
                await _service.UpdateCategoryAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Category not found");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await _service.DeleteCategoryAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Category not found");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
