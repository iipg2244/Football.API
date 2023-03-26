using Microsoft.Extensions.Configuration;

namespace Football.API.Models.Others
{
    public class Methods
    {
        public static IConfigurationRoot GetConfigurationRoot()
        {
#if DEBUG
            var appsettings = new ConfigurationBuilder().SetBasePath(System.AppContext.BaseDirectory)
              .AddJsonFile("appsettings.Development.json", optional: true);
#else
            var appsettings = new ConfigurationBuilder().SetBasePath(System.AppContext.BaseDirectory)
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
#endif
            return appsettings.Build();
        }
    }
}
