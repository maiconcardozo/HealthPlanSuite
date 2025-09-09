using HealthPlan.Quote.Infrastructure.Data;
using HealthPlan.Quote.Repository.Implementation;
using HealthPlan.Quote.Repository.Interface;
using HealthPlan.Quote.Services.Implementation;
using HealthPlan.Quote.Services.Interface;
using HealthPlan.Quote.UnitOfWork.Implementation;
using HealthPlan.Quote.UnitOfWork.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql;

namespace HealthPlan.Quote.Extensions
{
    public static class AuthenticationLoginServiceCollectionExtensions
    {
        public static IServiceCollection AddAuthenticationLoginServices(this IServiceCollection services, string connectionString)
        {
            // Check if we're in test environment to avoid MySQL connection
            if (string.IsNullOrEmpty(connectionString) || connectionString.Contains("InMemoryDbForTesting"))
            {
                // Use in-memory database for testing
                services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("InMemoryDbForTesting"));
            }
            else
            {
                // Use MySQL for production/development
                services.AddDbContext<ApplicationContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
            }
            
            services.AddScoped<DbContext>(provider => provider.GetRequiredService<ApplicationContext>());

            // Services
            services.AddScoped<ICleanEntityService, CleanEntityService>();

            // Repositories
            services.AddScoped<ICleanEntityRepository, CleanEntityRepository>();

            // Unit of Work
            services.AddScoped<IApplicationUnitOfWork, ApplicationUnitOfWork>();

            return services;
        }
    }
}