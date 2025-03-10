using FSA_3S.Entity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using BCrypt.Net;

namespace FSA_3S.Service
{
    public class UserService
    {
        private readonly AppDbContext _appDbContext;
        private readonly EmailService _emailService;

        public UserService(AppDbContext appDbContext, EmailService emailService)
        {
            _appDbContext = appDbContext;
            _emailService = emailService;
        }

        public async Task<string> CreateEmployeeAsync(string email, string role)
        {
            if (await _appDbContext.Users.AnyAsync(u => u.Email == email))
            {
                return "Email đã có trong hệ thống!";
            }

            string password = GenerateRandomPassword();

            
            string mhPassword = HashPassword(password);

            var user = new User
            {
                Email = email,
                Password = mhPassword,  
                Role = role,
                CreateDate = DateTime.UtcNow
            };

            _appDbContext.Users.Add(user);
            await _appDbContext.SaveChangesAsync();

            string subject = "Thông tin tài khoản của hệ thống 3S";
            string message = $"3S, Xin chào,\n\nTài khoản của bạn đã được tạo:\nEmail: {email}\nMật khẩu: {password}\n\nVui lòng đăng nhập và đổi mật khẩu ngay lập tức.";

            await _emailService.SendEmailUser(email, subject, message);
            return "Tạo tài khoản thành công!";
        }

        private string GenerateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 8);
        }

 
        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
