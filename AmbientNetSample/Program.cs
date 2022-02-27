using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using Polly.Extensions.Http;


namespace AmbientNetSample
{
    public class Program
    {
        private static async Task Main()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsetting.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var builder = new HostBuilder()
                .ConfigureServices((_, services) =>
                {
                    services.AddTransient<Sample>();
                    services.AddHttpClient<Sample>()
                        .AddPolicyHandler(GetRetryPolicy());
                    services.Configure<AppOptions>(config.GetSection(@"appSettings"));
                }).UseConsoleLifetime();

            var host = builder.Build();

            using var serviceScope = host.Services.CreateScope();
            {
                var services = serviceScope.ServiceProvider;

                try
                {
                    var sample = services.GetRequiredService<Sample>();
                    await sample.RunAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex}");
                }
            }
        }


        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            var jitterier = new Random();

            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.Forbidden)
                .WaitAndRetryAsync(5,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)) + TimeSpan.FromMilliseconds(jitterier.Next(0, 100)),
                    (response, delay, retryCount, context) =>
                    {
                        Console.WriteLine($"Retrying: StatusCode: {response.Result.StatusCode} Message: {response.Result.ReasonPhrase} RequestUri: {response.Result.RequestMessage?.RequestUri}");
                    });
        }
    }
}
