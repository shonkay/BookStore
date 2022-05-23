using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                          .MinimumLevel.Verbose()
                          .WriteTo.File("C:\\BookStore Log\\Log.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: null,
                           outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss. fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                          .CreateLogger();
            Log.Logger.Information("Executed at {ExecutedTime}", Environment.TickCount);

            try
            {
                CreateHostBuilder(args).Build().Run();
            }
           finally
            {

                Log.CloseAndFlush();
            }
           
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
