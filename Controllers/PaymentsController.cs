using Microsoft.AspNetCore.Mvc;
using store_management.DTO;
using store_management.Service;

namespace store_management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _service;

        public PaymentsController(IPaymentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<paymentsDTO>>> GetAllPayments()
        {
            var payments = await _service.GetAllPaymentsAsync();
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<paymentsDTO>> GetPaymentById(int id)
        {
            try
            {
                var payment = await _service.GetPaymentByIdAsync(id);
                return Ok(payment);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Payment not found");
            }
        }

        [HttpPost]
        public async Task<ActionResult<paymentsDTO>> CreatePayment(paymentsDTO dto)
        {
            try
            {
                var payment = await _service.CreatePaymentAsync(dto);
                return CreatedAtAction(nameof(GetPaymentById), new { id = payment.Payment_id }, payment);
            }
            catch (Exception ex)  // Sửa: Catch Exception thay vì multiple types
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(int id, paymentsDTO dto)
        {
            try
            {
                await _service.UpdatePaymentAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Payment not found");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            try
            {
                await _service.DeletePaymentAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Payment not found");
            }
        }
    }
}