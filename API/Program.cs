using System;
using API.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        public static void Main(string[] args) //start
        {
            var host = CreateHostBuilder(args).Build(); //hosted by kestral, creates kestral server with some default settings
            using var scope = host.Services.CreateScope(); // scope created from services, using keyword disposes of scope
            var context = scope.ServiceProvider.GetRequiredService<StoreContext>(); // use scope to access store context
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>(); // log to terminal any errors
            try
            {
                context.Database.Migrate(); // automatically move forward with any migrations
                DbInitializer.Initialize(context); // gives dbinitializer the context to add products to database
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Problem migrating data");
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => //method
            Host.CreateDefaultBuilder(args) // configures it with some defaults
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>(); //starts it with a starter class (startup.cs)
                });
    }
}
