using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Api.Db;
using static System.Runtime.InteropServices.JavaScript.JSType;
using TodoApp.Api.Db.RequestEntities;
using TodoApp.Api.Models.Requests;
using Microsoft.EntityFrameworkCore;

namespace EmailSenderConsole.EmailSender
{

    public class EmailSender
    {
        private readonly AppSettings _appSettings;

        public EmailSender(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }
        private void SendEmail(BaseRequestEntity emailEntity)
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
                Credentials = new NetworkCredential(_appSettings.CompanyEmail, "hjxzxpocutxmpcus"),
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

        public List<BaseRequestEntity> SendEmailRequests(List<BaseRequestEntity> requests)
        {
            // Counts errors for specific email address
            int errorCounter = 0;

            var deprecatedRequests = new List<BaseRequestEntity>();

            for (int i = 0; i < requests.Count; i++)
            {
                var request = requests[i];
                try
                {
                    if (request.CreatedAt.AddMinutes(15) < DateTime.UtcNow)
                    {
                        deprecatedRequests.Add(request);
                    }
                    else if (request.Status != RequestStatus.Sent && request.Status != RequestStatus.Failed)
                    {
                        Console.WriteLine($"Sending request to email - {request.ToAddress}");
                        SendEmail(request);
                        request.Status = RequestStatus.Sent;
                    }
                }
                catch (SmtpException e)
                {
                    errorCounter++;
                    i--;
                    if (errorCounter == 3)
                    {
                        errorCounter = 0;
                        request.Status = RequestStatus.Failed;
                        Console.WriteLine($"{request.ToAddress} status changed to ,,Failed''");
                    }
                    Console.WriteLine($"Error description: {e.Message}");
                    Console.WriteLine();
                }
                Thread.Sleep(5000);
            }

            return deprecatedRequests;

        }



    }
}
