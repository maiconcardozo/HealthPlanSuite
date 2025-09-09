using HealthPlan.Quote.Domain.Implementation;
using Microsoft.EntityFrameworkCore;

namespace HealthPlan.Quote.Infrastructure.Interface
{
    public interface IApplicationContext
    {
        public DbSet<CleanEntity> dbCleanEntity { get; set; }
    }
}
