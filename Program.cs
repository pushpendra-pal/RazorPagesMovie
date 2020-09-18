using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RazorPagesMovie.Models;

namespace RazorPagesMovie
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            //Replacing the above with modified seeded database service

            var host = CreateHostBuilder(args).Build(); //creating host

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;  //What is this, why is it required?

                try
                {
                    SeedData.Initialize(services);
                }
                catch (Exception e)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(e, "An error occured seeding the database");
                }
            }
            host.Run(); //running the host after adding seed data.
            //Run Update-Database after doing this.

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
