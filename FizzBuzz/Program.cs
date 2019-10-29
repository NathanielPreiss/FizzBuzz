using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FizzBuzz
{
    internal class Program
    {
        private static void Main()
        {
            var webHost = new WebHostBuilder()
                .UseKestrel()
                .ConfigureAppConfiguration((context, configuration) =>
                {
                    configuration.AddJsonFile("appSettings.json", false, true);
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                .UseStartup<Startup>()
                .Build();

            webHost.Run();
        }
    }
}
