using App.Dtos;
using App.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace App.Services.EmailService
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

            public async Task SendEmailConfirm(string email, string subject, string htmlMessage)
            {
                  var message = new MimeMessage();
                  message.Sender = new MailboxAddress(_emailSettings.DisplayName, _emailSettings.UserName);
                  message.From.Add(new MailboxAddress(_emailSettings.DisplayName, _emailSettings.UserName));
                  message.To.Add(MailboxAddress.Parse(email));
                  message.Subject = subject;


                  var builder = new BodyBuilder();
                  builder.HtmlBody = htmlMessage;
                  message.Body = builder.ToMessageBody();

                  // dùng SmtpClient của MailKit
                  using var smtp = new MailKit.Net.Smtp.SmtpClient();

                  try
                  {
                        smtp.Connect(_emailSettings.SmtpServer, _emailSettings.Port, SecureSocketOptions.StartTls);
                        smtp.Authenticate(_emailSettings.UserName, _emailSettings.Password);
                        await smtp.SendAsync(message);
                  }

                  catch (Exception ex)
                  {
                        // Gửi mail thất bại, nội dung email sẽ lưu vào thư mục mailssave
                        System.IO.Directory.CreateDirectory("mailssave");
                        var emailsavefile = string.Format(@"mailssave/{0}.eml", Guid.NewGuid());
                        await message.WriteToAsync(emailsavefile);

                  }
                  smtp.Disconnect(true);
            }
            public Task SendSmsAsync(string number, string message)
            {
                  System.IO.Directory.CreateDirectory("smssave");
                  var emailsavefile = string.Format(@"smssave/{0}-{1}.txt", number, Guid.NewGuid());
                  System.IO.File.WriteAllTextAsync(emailsavefile, message);
                  return Task.FromResult(0);
            }
      }
}

