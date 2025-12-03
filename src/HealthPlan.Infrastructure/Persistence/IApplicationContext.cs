using HealthPlan.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HealthPlan.Infrastructure.Persistence
{
    public interface IApplicationContext
    {
        public DbSet<AgeRange> dbAgeRange { get; set; }
        public DbSet<Beneficiary> dbBeneficiary { get; set; }
        public DbSet<Company> dbCompany { get; set; }
        public DbSet<Coverage> dbCoverage { get; set; }
        public DbSet<Domain.Entities.HealthPlan> dbHealthPlan { get; set; }
        public DbSet<Domain.Entities.Quote> dbQuote { get; set; }

        /// <summary>
        /// Generic method to get a DbSet for any entity type
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <returns>DbSet for the entity type</returns>
        DbSet<T> Set<T>() where T : class;

        /// <summary>
        /// Saves all changes made in this context to the database
        /// </summary>
        /// <returns>Number of state entries written to the database</returns>
        int SaveChanges();

        /// <summary>
        /// Asynchronously saves all changes made in this context to the database
        /// </summary>
        /// <returns>Task representing the asynchronous operation with number of state entries written</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Releases the allocated resources of this context
        /// </summary>
        void Dispose();
    }
}
