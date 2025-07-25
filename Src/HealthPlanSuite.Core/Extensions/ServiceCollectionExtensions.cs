using HealthPlanSuite.Core.Infrastructure;
using HealthPlanSuite.Core.Mapping;
using HealthPlanSuite.Core.Repository;
using HealthPlanSuite.Core.Services;
using HealthPlanSuite.Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HealthPlanSuite.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHealthPlanSuiteCore(this IServiceCollection services, string connectionString)
        {
            // Add DbContext
            services.AddDbContext<HealthPlanSuiteDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            // Add AutoMapper
            services.AddAutoMapper(typeof(HealthPlanSuiteMappingProfile));

            // Add Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

            // Add Repositories
            services.AddScoped<IOperadoraRepository, OperadoraRepository>();
            services.AddScoped<ITipoPlanoRepository, TipoPlanoRepository>();
            services.AddScoped<IPlanoRepository, PlanoRepository>();
            services.AddScoped<IFaixaEtariaRepository, FaixaEtariaRepository>();
            services.AddScoped<ITabelaPrecoRepository, TabelaPrecoRepository>();
            services.AddScoped<IReajusteRepository, ReajusteRepository>();
            services.AddScoped<IEstabelecimentoSaudeRepository, EstabelecimentoSaudeRepository>();
            services.AddScoped<IPlanoAbrangenciaRepository, PlanoAbrangenciaRepository>();

            // Add Services
            services.AddScoped<IOperadoraService, OperadoraService>();
            // Add other services as they are created...

            return services;
        }
    }
}