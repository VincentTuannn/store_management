using Microsoft.AspNetCore.Mvc;
using store_management.DTO;
using store_management.Service;

namespace store_management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderItemService _service;

        public OrderItemsController(IOrderItemService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<order_itemsDTO>>> GetAllOrderItems()
        {
            var items = await _service.GetAllOrderItemsAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<order_itemsDTO>> GetOrderItemById(int id)
        {
            try
            {
                var item = await _service.GetOrderItemByIdAsync(id);
                return Ok(item);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("OrderItem not found");
            }
        }

        [HttpPost]
        public async Task<ActionResult<order_itemsDTO>> CreateOrderItem(order_itemsDTO dto)
        {
            try
            {
                var item = await _service.CreateOrderItemAsync(dto);
                return CreatedAtAction(nameof(GetOrderItemById), new { id = item.Order_item_id }, item);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderItem(int id, order_itemsDTO dto)
        {
            try
            {
                await _service.UpdateOrderItemAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("OrderItem not found");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            try
            {
                await _service.DeleteOrderItemAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("OrderItem not found");
            }
        }
    }
}