using Microsoft.EntityFrameworkCore;
using HealthPlanSuite.Domain.Implementation;
using HealthPlanSuite.Infrastructure.Interface;

namespace HealthPlanSuite.Infrastructure.Implementation
{
    public class HealthPlanSuiteContext : DbContext, IHealthPlanSuiteContext
    {
        public HealthPlanSuiteContext(DbContextOptions<HealthPlanSuiteContext> options) : base(options)
        {
        }

        // DbSets
        public DbSet<Operadora> Operadoras { get; set; }
        public DbSet<TipoPlano> TiposPlano { get; set; }
        public DbSet<FaixaEtaria> FaixasEtaria { get; set; }
        public DbSet<EstabelecimentoSaude> EstabelecimentosSaude { get; set; }
        public DbSet<Plano> Planos { get; set; }
        public DbSet<TabelaPreco> TabelasPreco { get; set; }
        public DbSet<Reajuste> Reajustes { get; set; }
        public DbSet<PlanoAbrangencia> PlanosAbrangencia { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply all configurations in this assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HealthPlanSuiteContext).Assembly);
        }
    }
}