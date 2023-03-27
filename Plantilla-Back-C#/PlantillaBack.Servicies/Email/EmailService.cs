using SendGrid.Helpers.Mail;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantillaBack.Services.Email
{
    public class EmailService : IEmailService
    {
        public async Task SendMail(string fromEmail, string toEmail, string subject, string text)
        {
            var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(fromEmail, fromEmail);
            var to = new EmailAddress(toEmail, toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, text, "");
            var response = await client.SendEmailAsync(msg);
        }
    }
}
