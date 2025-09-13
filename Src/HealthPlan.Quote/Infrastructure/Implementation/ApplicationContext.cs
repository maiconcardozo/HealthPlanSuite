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
            // Minimal configuration for demonstration
            modelBuilder.ApplyConfiguration(new AgeRangeMap());
            modelBuilder.ApplyConfiguration(new CompanyMap());
            // Note: Other mappings need to be fixed and can be added back later
        }

        // Implement the missing IApplicationContext methods
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}