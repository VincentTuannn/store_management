using Microsoft.AspNetCore.Mvc;
using store_management.DTO;
using store_management.Service;

namespace store_management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrdersController(IOrderService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<ordersDTO>>> GetAllOrders()
        {
            var orders = await _service.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ordersDTO>> GetOrderById(int id)
        {
            try
            {
                var order = await _service.GetOrderByIdAsync(id);
                return Ok(order);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Order not found");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ordersDTO>> CreateOrder([FromBody] CreateOrderRequest request)
        {
            try
            {
                var order = await _service.CreateOrderAsync(request.Order, request.Items); 
                return CreatedAtAction(nameof(GetOrderById), new { id = order.Order_id }, order);
            }
            catch (Exception ex)  
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateOrderStatus(int id, string status)
        {
            try
            {
                await _service.UpdateOrderStatusAsync(id, status);
                return NoContent();
            }
            catch (Exception ex)  // Sửa: Catch Exception
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                await _service.DeleteOrderAsync(id);
                return NoContent();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }

    public class CreateOrderRequest
    {
        public ordersDTO Order { get; set; }
        public List<order_itemsDTO> Items { get; set; }
    }
}