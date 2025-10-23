using Microsoft.AspNetCore.Mvc;
using store_management.DTO;
using store_management.Service;

namespace store_management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierService _service;

        public SuppliersController(ISupplierService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<suppliersDTO>>> GetAllSuppliers()
        {
            var suppliers = await _service.GetAllSuppliersAsync();
            return Ok(suppliers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<suppliersDTO>> GetSupplierById(int id)
        {
            try
            {
                var supplier = await _service.GetSupplierByIdAsync(id);
                return Ok(supplier);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Supplier not found");
            }
        }

        [HttpPost]
        public async Task<ActionResult<suppliersDTO>> CreateSupplier(suppliersDTO dto)
        {
            try
            {
                var supplier = await _service.CreateSupplierAsync(dto);
                return CreatedAtAction(nameof(GetSupplierById), new { id = supplier.Supplier_id }, supplier);
            }
            catch (Exception ex)  // Sửa: Catch Exception thay vì multiple types
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplier(int id, suppliersDTO dto)
        {
            try
            {
                await _service.UpdateSupplierAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Supplier not found");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            try
            {
                await _service.DeleteSupplierAsync(id);
                return NoContent();
            }
            catch (Exception ex)  // Sửa: Catch Exception thay vì multiple types
            {
                return BadRequest(ex.Message);
            }
        }
    }
}