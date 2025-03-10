using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FSA_3S.Service
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendEmailUser(string toEmail, string subject, string message)
        {
            try
            {
                var emailSetting = _configuration.GetSection("EmailSettings");

                var email = new MimeMessage();
                email.From.Add(new MailboxAddress("Admin", emailSetting["SenderEmail"]));
                email.To.Add(new MailboxAddress("", toEmail));
                email.Subject = subject;

                email.Body = new TextPart(MimeKit.Text.TextFormat.Html) 
                {
                    Text = message
                };

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(emailSetting["SmtpServer"], int.Parse(emailSetting["SmtpPort"]), SecureSocketOptions.Auto);
                await smtp.AuthenticateAsync(emailSetting["SenderEmail"], emailSetting["SenderPassword"]);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);

                _logger.LogInformation($" Email sent to {toEmail} successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error sending email: {ex.Message}");
            }
        }
    }
}
