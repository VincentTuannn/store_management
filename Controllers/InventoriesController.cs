using Microsoft.AspNetCore.Mvc;
using store_management.DTO;
using store_management.Service;

namespace store_management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoriesController : Controller
    {
        private readonly IInventoryService _service;

        public InventoriesController(IInventoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<inventoryDTO>>> GetAllInventories()
        {
            var inventories = await _service.GetAllInventoriesAsync();
            return Ok(inventories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<inventoryDTO>> GetInventoryById(int id)
        {
            try
            {
                var inventory = await _service.GetInventoryByIdAsync(id);
                return Ok(inventory);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Inventory not found");
            }
        }

        [HttpPut("{productId}/quantity")]
        public async Task<ActionResult<inventoryDTO>> UpdateInventoryQuantity(int productId, int quantity)
        {
            try
            {
                var inventory = await _service.UpdateInventoryQuantityAsync(productId, quantity);
                return Ok(inventory);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Inventory not found");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventory(int id)
        {
            try
            {
                await _service.DeleteInventoryAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Inventory not found");
            }
        }
    }
}
