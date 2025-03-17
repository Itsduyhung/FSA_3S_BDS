using FSA_3S.Services.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FSA_3S.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController(UserService userService) : ControllerBase
    {
        private readonly UserService _userService = userService;

        [HttpPost("taotaikhoan")]
        public async Task<IActionResult> CreateEmployee([FromBody] UserDto userDto)
        {
            try
            {
                var result = await _userService.CreateEmployeeAsync(userDto.Email, userDto.Role, userDto.FullName);
                return Ok(new { message = result });
            }
            catch (DbUpdateException ex)
            {
                // Lấy thông tin chi tiết từ InnerException
                var detailedError = ex.InnerException?.Message ?? ex.Message;
                return StatusCode(500, new
                {
                    message = "Lỗi server",
                    error = detailedError
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi server", error = ex.Message });
            }
        }

        public class UserDto
        {
            public string Email { get; set; }
            public string Role { get; set; }
            public string FullName { get; set; }
        }
    }
}