using HealthPlan.Quote.Infrastructure.HealthPlan.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HealthPlan.API.Data
{
    public abstract class BaseApiContext : DbContext
    {
        protected readonly IConfiguration _configuration;

        protected BaseApiContext(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                if (!string.IsNullOrEmpty(connectionString))
                {
                    optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            HealthPlanContext.LoadModel(modelBuilder);
        }
    }
}