using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MyERP.Services
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            await SendEmailAsync(message.Destination, message.Subject, message.Body);
        }

        public async Task SendEmailAsync(string email, string subject, string body)
        {
            var mailAccount = "dangthetai1612@gmail.com";
            var mailPassword = "dangthetai";

            var from = new MailAddress(mailAccount, "TMT Support");
            var to = new MailAddress(email);

            var message = new MailMessage(from, to);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = mailAccount,  // replace with valid value
                    Password = mailPassword  // replace with valid value
                };

                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(message);
            }
        }
    }
}
