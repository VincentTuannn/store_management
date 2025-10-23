using Microsoft.AspNetCore.Mvc;
using store_management.DTO;
using store_management.Service;

namespace store_management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PromotionsController : ControllerBase
    {
        private readonly IPromotionService _service;

        public PromotionsController(IPromotionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<promotionsDTO>>> GetAllPromotions()
        {
            var promotions = await _service.GetAllPromotionsAsync();
            return Ok(promotions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<promotionsDTO>> GetPromotionById(int id)
        {
            try
            {
                var promotion = await _service.GetPromotionByIdAsync(id);
                return Ok(promotion);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Promotion not found");
            }
        }

        [HttpPost]
        public async Task<ActionResult<promotionsDTO>> CreatePromotion(promotionsDTO dto)
        {
            try
            {
                var promotion = await _service.CreatePromotionAsync(dto);
                return CreatedAtAction(nameof(GetPromotionById), new { id = promotion.Promotion_id }, promotion);
            }
            catch (Exception ex)  // Sửa: Catch Exception thay vì multiple types
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePromotion(int id, promotionsDTO dto)
        {
            try
            {
                await _service.UpdatePromotionAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Promotion not found");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePromotion(int id)
        {
            try
            {
                await _service.DeletePromotionAsync(id);
                return NoContent();
            }
            catch (Exception ex)  // Sửa: Catch Exception thay vì multiple types
            {
                return BadRequest(ex.Message);
            }
        }
    }
}