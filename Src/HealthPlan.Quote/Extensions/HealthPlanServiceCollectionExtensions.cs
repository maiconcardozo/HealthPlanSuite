using HealthPlan.Quote.Repository.HealthPlan.Implementation;
using HealthPlan.Quote.Repository.HealthPlan.Interface;
using HealthPlan.Quote.Services.HealthPlan.Implementation;
using HealthPlan.Quote.Services.HealthPlan.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace HealthPlan.Quote.Extensions
{
    public static class HealthPlanServiceCollectionExtensions
    {
        public static IServiceCollection AddHealthPlanServices(this IServiceCollection services)
        {
            // Register repositories
            services.AddScoped<IHealthInsuranceOperatorRepository, HealthInsuranceOperatorRepository>();
            services.AddScoped<IPlanTypeRepository, PlanTypeRepository>();
            services.AddScoped<IHealthPlanRepository, HealthPlanRepository>();
            services.AddScoped<IAgeRangeRepository, AgeRangeRepository>();
            services.AddScoped<IPriceTableRepository, PriceTableRepository>();
            services.AddScoped<IPlanAdjustmentRepository, PlanAdjustmentRepository>();
            services.AddScoped<IHealthEstablishmentRepository, HealthEstablishmentRepository>();
            services.AddScoped<IPlanCoverageRepository, PlanCoverageRepository>();

            // Register services
            services.AddScoped<IHealthInsuranceOperatorService, HealthInsuranceOperatorService>();
            services.AddScoped<IHealthPlanService, HealthPlanService>();
            services.AddScoped<IPriceTableService, PriceTableService>();
            // Note: Add other service implementations as they are created

            return services;
        }
    }
}