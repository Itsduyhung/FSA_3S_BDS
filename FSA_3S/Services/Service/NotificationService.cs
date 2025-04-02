using FSA_3S.Models;
using FSA_3S.Repositories.Interface;
using FSA_3S.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace FSA_3S.Services.Service
{
    public class NotificationService : INotificationService
    {
        private readonly AppDbContext _context;

        public NotificationService(AppDbContext context)
        {
            _context = context;
        }

        // ✅ Lấy danh sách thông báo của người dùng
        public async Task<List<NotificationEntity>> GetNotificationsByUserIdAsync(int userId)
        {
            return await _context.Notifications
                .Where(n => n.ReceiverId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }

        // ✅ Xóa thông báo theo ID
        public async Task<bool> DeleteNotificationAsync(int notificationId)
        {
            var notification = await _context.Notifications.FindAsync(notificationId);
            if (notification == null) return false;

            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();
            return true;
        }

        // ✅ Cập nhật trạng thái thông báo (Read/Unread)
        public async Task<bool> UpdateNotificationStatusAsync(int notificationId, string status)
        {
            var notification = await _context.Notifications.FindAsync(notificationId);
            if (notification == null) return false;

            notification.IsRead = (status == "Read");
            await _context.SaveChangesAsync();
            return true;
        }

        // ✅ Gửi thông báo khi có bài đăng mới
        public async Task NotificationPostRealAsync(int senderId, int receiverId, string title, string message)
        {
            var notification = new NotificationEntity
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Title = title,
                Message = message,
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }

        // ✅ Gửi thông báo khi cập nhật bài đăng
        public async Task NotificationPutRealAsync(int senderId, int receiverId, string title, string message)
        {
            var notification = new NotificationEntity
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Title = title,
                Message = message,
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }
    }
}