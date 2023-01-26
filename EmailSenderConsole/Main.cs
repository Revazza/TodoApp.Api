using EmailSenderConsole;
using EmailSenderConsole.EmailSender;
using Microsoft.EntityFrameworkCore;
using TodoApp.Api.Db;
using TodoApp.Api.Db.RequestEntities;
using TodoApp.Api.Models.Requests;

var appSettings = new AppSettings();
var optionsBuilder = new DbContextOptionsBuilder<TodoAppDbContext>();
optionsBuilder.UseSqlServer(appSettings.DatabaseConnectionString);


using (var _context = new TodoAppDbContext(optionsBuilder.Options))
{
    var emailSender = new EmailSender(appSettings);

    while (true)
    {
        var emailRequests = _context.SendEmailRequests.ToList();
        var resetPasswordRequests = _context.SendResetPasswordRequests.ToList();

        var requests = new List<BaseRequestEntity>();
        requests.AddRange(emailRequests);
        requests.AddRange(resetPasswordRequests);

        var deprecatedRequests = emailSender.SendEmailRequests(requests);

        foreach (var request in deprecatedRequests)
        {
            if (request is SendEmailRequestEntity)
            {
                _context.SendEmailRequests.Remove(request as SendEmailRequestEntity);
            }
            else if (request is SendResetPasswordRequestEntity)
            {
                _context.SendResetPasswordRequests.Remove(request as SendResetPasswordRequestEntity);
            }
            Console.WriteLine($"Request with id - {request.Id} removed");
        }

        await _context.SaveChangesAsync();
    }




}




