using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailService
{
    public interface IEmailSender
    {
        Task<HttpStatusCode> SendMailAsync(string subject, string body, string to);
    }
}
