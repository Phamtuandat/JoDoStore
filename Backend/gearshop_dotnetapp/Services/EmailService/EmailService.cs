using gearshop_dotnetapp.Models.Identity;
using gearshop_dotnetapp.Resources;
using gearshop_dotnetapp.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace gearshop_dotnetapp.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly IHostEnvironment _env;
        public EmailService(IOptions<EmailSettings> emailSettings, IHostEnvironment env)
        {
            _emailSettings = emailSettings.Value;
            _env = env;
        }

        public async Task SendEmailAsync(ConfirmEmailRequest request)
        {
            string FilePath;
            if (_env.IsDevelopment())
            {
                FilePath = Directory.GetCurrentDirectory() + "\\StaticHtml\\wellcomePage.html";
            }
            else
            {
                FilePath = "/app/StaticHtml/wellcomePage.html";
            };
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[username]", request.UserName).Replace("[email]", request.ToEmail).Replace("[confirmLink]", request.ComfirmEmailLink);
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_emailSettings.UserName);
            email.To.Add(MailboxAddress.Parse(request.ToEmail));
            email.Subject = $"Welcome {request.UserName}";
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_emailSettings.SmtpServer, _emailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailSettings.UserName, _emailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}

