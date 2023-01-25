using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSenderConsole
{
    public class AppSettings
    {

        public string? DatabaseConnectionString { get; set; }
        public string? CompanyEmail { get; set; }


        public AppSettings()
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

            DatabaseConnectionString = configuration.GetConnectionString("TodoAppConnection");
            CompanyEmail = configuration["CompanyEmail"];
        }



    }
}
