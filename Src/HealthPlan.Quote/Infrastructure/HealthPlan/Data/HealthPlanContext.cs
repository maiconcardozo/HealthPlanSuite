using HealthPlan.Quote.Domain.HealthPlan.Implementation;
using HealthPlan.Quote.Infrastructure.HealthPlan.Implementation;
using Microsoft.EntityFrameworkCore;

namespace HealthPlan.Quote.Infrastructure.HealthPlan.Data
{
    public class HealthPlanContext : DbContext
    {
        public HealthPlanContext(DbContextOptions<HealthPlanContext> options) : base(options)
        {
        }

        public DbSet<HealthInsuranceOperator> HealthInsuranceOperators { get; set; }
        public DbSet<PlanType> PlanTypes { get; set; }
        public DbSet<Domain.HealthPlan.Implementation.HealthPlan> HealthPlans { get; set; }
        public DbSet<AgeRange> AgeRanges { get; set; }
        public DbSet<PriceTable> PriceTables { get; set; }
        public DbSet<PlanAdjustment> PlanAdjustments { get; set; }
        public DbSet<HealthEstablishment> HealthEstablishments { get; set; }
        public DbSet<PlanCoverage> PlanCoverages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            LoadModel(modelBuilder);
        }

        public static void LoadModel(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new HealthInsuranceOperatorMap());
            modelBuilder.ApplyConfiguration(new PlanTypeMap());
            modelBuilder.ApplyConfiguration(new HealthPlanMap());
            modelBuilder.ApplyConfiguration(new AgeRangeMap());
            modelBuilder.ApplyConfiguration(new PriceTableMap());
            modelBuilder.ApplyConfiguration(new PlanAdjustmentMap());
            modelBuilder.ApplyConfiguration(new HealthEstablishmentMap());
            modelBuilder.ApplyConfiguration(new PlanCoverageMap());
        }
    }
}