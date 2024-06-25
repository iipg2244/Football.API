namespace Football.API
{
    using Football.Infrastructure;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Serilog.Sinks.Elasticsearch;
    using Serilog;
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Football.Domain.Shared;
    using Football.Infrastructure.Mappings;

    public class Program
    {
        protected Program()
        {
        }

        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            ConfigureLogging();

            CreateDbIfNotExists(host);

            host.Run();
        }

        private static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<FootballContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }

        private static void ConfigureLogging()
        {
            var configuration = Methods.GetConfigurationRoot();
            var environment = Methods.GetAspNetCoreEnvironment();
            Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .WriteTo.Debug()
            .WriteTo.Console()
            .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment))
            .Enrich.WithProperty("Environment", environment)
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
        }

        private static ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment) => new ElasticsearchSinkOptions(new Uri(configuration.GetConnectionString("ElasticSearchConnection")))
        {
            AutoRegisterTemplate = true,
            IndexFormat = $"logging-{environment.ToLower()}-{DateTime.UtcNow:yyyy.MM.dd}"
        };

        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            }).ConfigureAppConfiguration(configuration =>
            {
                configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                configuration.AddJsonFile(
                    $"appsettings.{Methods.GetAspNetCoreEnvironment()}.json",
                    optional: true);
            })
            .UseSerilog();
    }
}
