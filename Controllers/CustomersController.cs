using Microsoft.AspNetCore.Mvc;
using store_management.DTO;
using store_management.Service;

namespace store_management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomersController(ICustomerService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<customersDTO>>> GetAllCustomers()
        {
            var customers = await _service.GetAllCustomersAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<customersDTO>> GetCustomerById(int id)
        {
            try
            {
                var customer = await _service.GetCustomerByIdAsync(id);
                return Ok(customer);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Customer not found");
            }
        }

        [HttpPost]
        public async Task<ActionResult<customersDTO>> CreateCustomer(customersDTO dto)
        {
            try
            {
                var customer = await _service.CreateCustomerAsync(dto);
                return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Customer_id }, customer);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, customersDTO dto)
        {
            try
            {
                await _service.UpdateCustomerAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Customer not found");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                await _service.DeleteCustomerAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Customer not found");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}