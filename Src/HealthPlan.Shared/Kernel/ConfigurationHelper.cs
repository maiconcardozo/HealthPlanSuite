using HealthPlan.Shared.Constants;
using Microsoft.Extensions.Configuration;

namespace HealthPlan.Shared.Helpers
{
    public static class ConfigurationHelper
    {
        public static string GetConnectionString(IConfigurationBuilder appsettings, string connectionName = ApplicationConstants.DefaultConnectionStringName)
        {
            var configuration = appsettings.Build();
            return GetConnectionString(configuration, connectionName);
        }

        public static string GetConnectionString(IConfiguration configuration, string connectionName = ApplicationConstants.DefaultConnectionStringName)
        {
            var isTest = AppDomain.CurrentDomain.GetAssemblies()
                .Any(a => a.FullName?.StartsWith("xunit", StringComparison.OrdinalIgnoreCase) == true ||
                          a.FullName?.StartsWith("nunit", StringComparison.OrdinalIgnoreCase) == true ||
                          a.FullName?.StartsWith("Microsoft.VisualStudio.TestPlatform", StringComparison.OrdinalIgnoreCase) == true);

            return isTest ? "InMemoryDbForTesting" : configuration.GetConnectionString(connectionName) ?? string.Empty;
        }

        public static IConfigurationSection GetJwtSettings(IConfigurationBuilder appsettings)
        {
            var configuration = appsettings.Build();
            return GetJwtSettings(configuration);
        }

        public static IConfigurationSection GetJwtSettings(IConfiguration configuration)
        {
            return configuration.GetSection(ApplicationConstants.JwtSettingsSection);
        }
    }
}
