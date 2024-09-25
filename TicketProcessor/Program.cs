using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using TicketApi.Data;
using TicketProcessor.Services;

namespace TicketProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var processingService = services.GetRequiredService<TicketProcessingService>();

                while (true)
                {
                    processingService.ProcessTicketsAsync().Wait();
                    Console.WriteLine("Processed tickets at: " + DateTime.UtcNow);
                    Thread.Sleep(60000); // 60 seconds
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddDbContext<TicketDbContext>(options =>
                        options.UseSqlite("Data Source=../TicketApi/tickets.db"));
                    services.AddTransient<TicketProcessingService>();
                });
    }
}