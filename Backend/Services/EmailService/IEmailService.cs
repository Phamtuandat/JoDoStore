
using App.Dtos;

namespace App.Services.EmailService
{
      public interface IEmailService
      {
            Task SendEmailAsync(ConfirmEmailRequest request);
            Task SendEmailConfirm(string email, string subject, string message);
            Task SendSmsAsync(string number, string message);
      }
}