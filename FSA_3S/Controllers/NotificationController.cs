using FSA_3S.Models;
using FSA_3S.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FSA_3S.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        // API để lấy danh sách thông báo của người dùng
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetNotifications(int userId)
        {
            var notifications = await _notificationService.GetNotificationsByUserIdAsync(userId);
            if (notifications == null || notifications.Count == 0)
            {
                return NotFound("No notifications found.");
            }

            return Ok(notifications);
        }

        // API để xóa thông báo theo ID
        [HttpDelete("{notificationId}")]
        public async Task<IActionResult> DeleteNotification(int notificationId)
        {
            var success = await _notificationService.DeleteNotificationAsync(notificationId);
            if (!success)
            {
                return NotFound("Notification not found.");
            }

            Response.Headers.Add("X-Message", "Notification deleted successfully.");
            return NoContent();
        }

        // API để chuyển trạng thái thông báo thành Unread hoặc Read
        [HttpPut("{notificationId}/status")]
        public async Task<IActionResult> UpdateNotificationStatus(int notificationId, [FromBody] string status)
        {
            if (status != "Read" && status != "Unread")
            {
                return BadRequest("Invalid status value. Must be 'Read' or 'Unread'.");
            }

            var success = await _notificationService.UpdateNotificationStatusAsync(notificationId, status);
            if (!success)
            {
                return NotFound("Notification not found.");
            }

            return Ok("Notification status updated successfully.");
        }
    }
}