using Microsoft.AspNetCore.Identity.UI.Services;

namespace WebAppliNaissance.Services
{
    public class NewEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            Console.WriteLine($"Email fictif envoyé à : {email}");
            Console.WriteLine($"Sujet : {subject}");
            Console.WriteLine($"Message : {htmlMessage}");
            return Task.CompletedTask;
        }

    }
}
