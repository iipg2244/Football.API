namespace Football.IntegrationTests.Configuration
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using Football.API;
    using Football.Domain.Contracts.Refit;
    using Football.Infrastructure;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Refit;
    using Serilog;

    public class CustomWebApplicationFactory : WebApplicationFactory<Startup>
    {
        private HttpClient _httpClient;

        public HttpClient GetHttpClient()
        {
            if (_httpClient == null)
            {
                var clientOptions = new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false
                };
                _httpClient = CreateClient(clientOptions);
            }

            return _httpClient;
        }

        public T GetService<T>() where T : notnull => Services.GetRequiredService<T>();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            string environment = "Testing";
            builder.UseEnvironment(environment);
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", environment);

            builder.ConfigureAppConfiguration((context, config) => { 
                config.Sources.Clear();
                config.SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: false)
                .AddEnvironmentVariables();
            })
            .ConfigureLogging((context, builder) =>
            {
                var logger = new LoggerConfiguration().ReadFrom.Configuration(context.Configuration);
                builder.ClearProviders().AddSerilog(logger.CreateLogger(), dispose: true);
            })
            .ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<FootballContext>));

                services.Remove(descriptor);

                services.AddDbContext<FootballContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<FootballContext>();

                    db.Database.EnsureCreated();
                }

                services.AddSingleton<IManagerController>(_ =>
                {
                    return RestService.For<IManagerController>(GetHttpClient(), new RefitSettings());
                });
            });
        }
    }
}
