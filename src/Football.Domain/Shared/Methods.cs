namespace Football.Domain.Shared
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Configuration.Json;
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public static class Methods
    {
        public static string GetAspNetCoreEnvironment() => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? string.Empty;

        public static IConfigurationRoot GetConfigurationRoot()
        {
            IConfigurationBuilder appsettings = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{GetAspNetCoreEnvironment()}.json", optional: true);
            return appsettings.Build();
        }
    }
}
