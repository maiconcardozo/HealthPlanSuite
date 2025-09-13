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

        // Health plan domain entities
        public DbSet<AgeRange> dbAgeRange { get; set; }
        public DbSet<Beneficiary> dbBeneficiary { get; set; }
        public DbSet<Company> dbCompany { get; set; }
        public DbSet<Coverage> dbCoverage { get; set; }
        public DbSet<Domain.Implementation.HealthPlan> dbHealthPlan { get; set; }
        public DbSet<Domain.Implementation.Quote> dbQuote { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            LoadModel(modelBuilder);
        }

        public static void LoadModel(ModelBuilder modelBuilder)
        {
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