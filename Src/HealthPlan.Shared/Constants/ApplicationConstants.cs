namespace HealthPlan.Shared.Constants
{
    public static class ApplicationConstants
    {
        public const string DefaultCreatedByUser = "admin";
        public const int DefaultTokenExpirationHours = 1;
        public const string DefaultConnectionStringName = "DefaultConnection";
        public const string JwtSettingsSection = "JwtSettings";

        public static class ClaimTypes
        {
            public const string Permission = "permission";
        }

        public static class Environment
        {
            public const string Production = "Production";
            public const string Development = "Development";
        }

        public static class Cors
        {
            public const string AllowAllPolicy = "AllowAll";
        }

        public static class Api
        {
            public const string Title = "Health Plan API";
            public const string Version = "v1";
            public const string SwaggerEndpoint = "/swagger/v1/swagger.json";
            public const string SwaggerDisplayName = "Health Plan API V1";
            public const string CustomStylePath = "/Style/custom-swagger.css";
            public const string EmptyRoutePrefix = "";

            public static class SwaggerDefinitions
            {
                public const string Authentication = "HealthPlan";
                public const string AuthenticationEndpoint = "/swagger/HealthPlan/swagger.json";
                public const string AuthenticationDisplayName = "Health Plan API";
            }
        }
    }
}
