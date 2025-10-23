using Microsoft.AspNetCore.Mvc;
using store_management.DTO;
using store_management.Service;

namespace store_management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<usersDTO>>> GetAllUsers()
        {
            var users = await _service.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<usersDTO>> GetUserById(int id)
        {
            try
            {
                var user = await _service.GetUserByIdAsync(id);
                return Ok(user);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("User not found");
            }
        }

        [HttpPost]
        public async Task<ActionResult<usersDTO>> CreateUser(usersDTO dto)
        {
            try
            {
                var user = await _service.CreateUserAsync(dto);
                return CreatedAtAction(nameof(GetUserById), new { id = user.User_id }, user);
            }
            catch (Exception ex)  
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, usersDTO dto)
        {
            try
            {
                await _service.UpdateUserAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("User not found");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _service.DeleteUserAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("User not found");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
        }
    }
}