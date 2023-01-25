using EmailSenderConsole;
using EmailSenderConsole.EmailSender;
using Microsoft.EntityFrameworkCore;
using System.Collections.Specialized;
using System.Net.Mail;
using TodoApp.Api.Db;
using TodoApp.Api.Db.Entities;

var appSettings = new AppSettings();
var optionsBuilder = new DbContextOptionsBuilder<TodoAppDbContext>();
optionsBuilder.UseSqlServer(appSettings.DatabaseConnectionString);



using (var _context = new TodoAppDbContext(optionsBuilder.Options))
{

    var emailSender = new EmailSender(appSettings);

    // Counts errors for specific email address
    var errorCounter = 0;

    while (true)
    {
        var sendEmailRequests = _context.SendEmailRequests.ToList();

        if (sendEmailRequests.Count == 0)
        {
            Console.WriteLine("No emails to send");
        }
        else
        {
            for (int i = 0; i < sendEmailRequests.Count; i++)
            {
                var emailRequest = sendEmailRequests[i];
                Console.WriteLine(emailRequest);
                try
                {
                    if (emailRequest.Status == EmailStatus.Failed)
                    {
                        continue;
                    }

                    emailSender.SendEmail(emailRequest);

                    _context.SendEmailRequests.Remove(emailRequest);
                    await _context.SaveChangesAsync();
                }
                catch (SmtpException e)
                {
                    errorCounter++;
                    //looping till we send email
                    i--;

                    // Checking if email sent failed 3 times in a row
                    if (errorCounter == 3)
                    {
                        errorCounter = 0;
                        emailRequest.Status = EmailStatus.Failed;
                        Console.WriteLine($"{emailRequest.ToAddress} status changed to ,,Failed''");
                        await _context.SaveChangesAsync();
                        continue;
                    }

                    Console.WriteLine($"Error Occured while sending message to email: {emailRequest.ToAddress}");
                    Console.WriteLine($"Error Description: " + e.Message);
                    Console.WriteLine();
                    Thread.Sleep(5000);
                }
            }
        }


        Thread.Sleep(5000);

    }




}




