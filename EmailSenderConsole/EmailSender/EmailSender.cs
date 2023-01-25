using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Api.Db;
using TodoApp.Api.Db.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EmailSenderConsole.EmailSender
{

    public class EmailSender
    {
        private readonly AppSettings _appSettings;

        public EmailSender(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }
        public void SendEmail(SendEmailRequestEntity emailEntity)
        {

            var to = new MailAddress(emailEntity.ToAddress!);
            var from = new MailAddress(_appSettings.CompanyEmail!);

            var message = new MailMessage(from, to)
            {
                Subject = emailEntity.Subject,
                Body = emailEntity.Body
            };

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(_appSettings.CompanyEmail, "juqkyhbvcoxmoakm"),
                EnableSsl = true,
            };
            try
            {
                client.Send(message);
            }
            catch (SmtpException ex)
            {
                throw new SmtpException(ex.Message);
            }

        }
    }
}
