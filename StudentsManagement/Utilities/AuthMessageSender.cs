using Microsoft.AspNetCore.Identity.UI.Services;
using StudentsManagement.Data;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace StudentsManagement.Utilities
{
    public class AuthMessageSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public AuthMessageSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            // Sử dụng tài khoản SMTP cố định
            var smtpClient = new SmtpClient(_configuration["Smtp:Host"])
            {
                Port = int.Parse(_configuration["Smtp:Port"]),
                Credentials = new NetworkCredential(_configuration["Smtp:Username"], _configuration["Smtp:Password"]),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                // Thay đổi "From" để hiển thị email của người dùng đăng ký
                From = new MailAddress(_configuration["Smtp:From"]),
                Subject = subject,
                Body = message,
                IsBodyHtml = true,
            };
            // Đặt email người nhận (ví dụ: admin tổng)
            mailMessage.To.Add(email);

            await smtpClient.SendMailAsync(mailMessage);
        }

        // Phương thức gửi email xác nhận
        //public async Task SendConfirmationLinkAsync(ApplicationUser user, string email, string callbackUrl)
        //{
        //    var subject = "Confirm your email";
        //    var message = $"Please confirm your account by <a href='{callbackUrl}'>clicking here</a>.";

        //    await SendEmailAsync(email, subject, message);
        //}
    }
}
