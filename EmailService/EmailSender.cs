using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmailService
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfiguration;

        public EmailSender(EmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }

        public async Task<HttpStatusCode> SendMailAsync(string subject, string body, string to)
        {
            try
            {
                string apiKey = _emailConfiguration.ApiKey;
                string from = _emailConfiguration.From;

                SendGridMessage msg = MailHelper.CreateSingleEmail(new EmailAddress(from), new EmailAddress(to), subject, "", body);

                SendGridClient client = new SendGridClient(apiKey);
                Response? response = await client.SendEmailAsync(msg);

                HttpStatusCode statusCode = response.StatusCode;
                return statusCode;

                //var responseBody = response.Body.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
