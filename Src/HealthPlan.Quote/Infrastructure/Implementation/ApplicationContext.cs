using HealthPlan.Quote.Domain.Implementation;
using HealthPlan.Quote.Infrastructure.Implementation;
using HealthPlan.Quote.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

namespace HealthPlan.Quote.Infrastructure.Data
{
    public class ApplicationContext : DbContext, IApplicationContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        // Existing entity
        public DbSet<CleanEntity> dbCleanEntity { get; set; }
        
        // Health plan domain entities
        public DbSet<Domain.Implementation.Quote> Quotes { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Beneficiary> Beneficiaries { get; set; }
        public DbSet<AgeRange> AgeRanges { get; set; }
        public DbSet<Coverage> Coverages { get; set; }
        public DbSet<Domain.Implementation.HealthPlan> HealthPlans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            LoadModel(modelBuilder);
        }

        public static void LoadModel(ModelBuilder modelBuilder)
        {
            // Existing mapping
            modelBuilder.ApplyConfiguration(new CleanEntityMap());
            
            // Health plan domain mappings
            modelBuilder.ApplyConfiguration(new QuoteMap());
            modelBuilder.ApplyConfiguration(new CompanyMap());
            modelBuilder.ApplyConfiguration(new BeneficiaryMap());
            modelBuilder.ApplyConfiguration(new AgeRangeMap());
            modelBuilder.ApplyConfiguration(new CoverageMap());
            modelBuilder.ApplyConfiguration(new HealthPlanMap());
        }
    }
}