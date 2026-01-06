using HealthPlan.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HealthPlan.API.Data
{
    public abstract class BaseApiContext : DbContext
    {
        private readonly IConfiguration configuration;

        protected BaseApiContext(IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                if (!string.IsNullOrEmpty(connectionString))
                {
                    if (connectionString.Contains("InMemoryDbForTesting", StringComparison.OrdinalIgnoreCase))
                    {
                        optionsBuilder.UseInMemoryDatabase("InMemoryDbForTesting");
                    }
                    else
                    {
                        optionsBuilder.UseMySQL(connectionString);
                    }
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ApplicationContext.LoadModel(modelBuilder);
        }
    }
}
