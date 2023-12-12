using Excellerent.SharedInfrastructure.Context;
using Excellerent.TestData;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace excellerent_epp_be
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scoped = host.Services.CreateScope())
            {
                var services = scoped.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    var context = services.GetRequiredService<EPPContext>();
                    context.Database.SetCommandTimeout(3000);
                    context.Database.EnsureCreated();
                    await context.Database.MigrateAsync();
                    //await EppContextSeed.SeedAsync(context, loggerFactory);
                    await InitializeTestData(host, loggerFactory);

                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "An Error has Ocuured during Migrations");
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseWebRoot("..\\Excellerent.SharedInfrastructure\\SharedFiles");
                    webBuilder.UseStartup<Startup>();
                });

        private async static Task InitializeTestData(IHost host, ILoggerFactory loggerFactory)
        {
            using (var scope = host.Services.CreateScope())
            {
                var logger = loggerFactory.CreateLogger<Program>();
                var services = scope.ServiceProvider;
                try
                {
                    var testDataService = services.GetRequiredService<ITestDataService>();
                    await testDataService.Clear();
                    await testDataService.Add();
                    logger.LogError("This is a development environment!");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "This is a production environment!");
                }
            }
        }
    }
}
