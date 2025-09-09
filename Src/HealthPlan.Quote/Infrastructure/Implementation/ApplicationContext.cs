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

        public DbSet<CleanEntity> dbCleanEntity { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            LoadModel(modelBuilder);
        }

        public static void LoadModel(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CleanEntityMap());
        }
    }
}