namespace HealthPlan.API.Helper
{
    public static class Utils
    {
        public static string GetConnectionString(IConfigurationBuilder appsettings, string connectionName = "DefaultConnection")
        {
            var configuration = appsettings.Build();
            return configuration.GetConnectionString(connectionName);
        }
    }
}
