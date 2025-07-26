using Microsoft.Extensions.DependencyInjection;
using HealthPlanSuite.Services.Interface;
using HealthPlanSuite.Services.Implementation;
using HealthPlanSuite.UnitOfWork.Interface;
using HealthPlanSuite.UnitOfWork.Implementation;
using HealthPlanSuite.Infrastructure.Interface;
using HealthPlanSuite.Infrastructure.Implementation;

namespace HealthPlanSuite.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHealthPlanSuiteServices(this IServiceCollection services)
        {
            // Infrastructure
            services.AddScoped<IHealthPlanSuiteContext, HealthPlanSuiteContext>();
            services.AddScoped<IHealthPlanSuiteUnitOfWork, HealthPlanSuiteUnitOfWork>();

            // Services
            services.AddScoped<IOperadoraService, OperadoraService>();
            // Add other services here as they are implemented
            // services.AddScoped<IPlanoService, PlanoService>();
            // services.AddScoped<ITipoPlanoService, TipoPlanoService>();
            // etc.

            return services;
        }
    }
}