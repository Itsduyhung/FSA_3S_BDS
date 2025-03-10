using FSA_3S.Service;
using Microsoft.AspNetCore.Mvc;

namespace FSA_3S.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly UserService _userService;
        public AdminController(UserService userService)
        {
            _userService = userService;
        }
        [HttpPost("taotaikhoan")]
        public async Task<IActionResult> CreateEmployee([FromBody] UserDto userDto)
        {
            try
            {
                var result = await _userService.CreateEmployeeAsync(userDto.Email, userDto.Role);
                return Ok(new { message = result });
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
        }
    }

}
