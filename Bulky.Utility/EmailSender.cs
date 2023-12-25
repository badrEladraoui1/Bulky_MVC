using Microsoft.AspNetCore.Identity.UI.Services;

namespace Bulky.Utility
{
    public class EmailSender : IEmailSender
    {
        Task IEmailSender.SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //logic to send email in the futur
            return Task.CompletedTask;
        }
    }
}
