
using gearshop_dotnetapp.Resources;

namespace gearshop_dotnetapp.Services.EmailService
{
    public interface IEmailService
    {
        Task SendEmailAsync(ConfirmEmailRequest request);
    }
}