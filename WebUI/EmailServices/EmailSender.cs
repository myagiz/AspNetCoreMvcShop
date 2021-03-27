using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace WebUI.EmailServices
{
    public class EmailSender:IEmailSender
    {
        private const string SendGridKey = "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";
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(SendGridKey, subject, htmlMessage, email);
        }

        private async Task Execute(string sendGridKey, string subject, string htmlMessage, string email)
        {
            var client = new SendGridClient(sendGridKey);
            var from = new EmailAddress("test@example.com", "Example User");
            subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("test@example.com", "Example User");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);

        }
    }
}
