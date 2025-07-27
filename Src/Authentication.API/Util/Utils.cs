namespace Authentication.API.Helper
{
    public static class Utils
    {
        public static string GetConnectionString(IConfigurationBuilder appsettings, string connectionName = "DefaultConnection")
        {
            var configuration = appsettings.Build();
            return configuration.GetConnectionString(connectionName);
        }

        internal static IConfigurationSection GetJwtSettings(IConfigurationBuilder appsettings)
        {
            var configuration = appsettings.Build();
            return configuration.GetSection("JwtSettings");
        }
    }
}
