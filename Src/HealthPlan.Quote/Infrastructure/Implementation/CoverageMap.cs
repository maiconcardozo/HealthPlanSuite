using HealthPlan.Quote.Domain.Implementation;
using Foundation.Base.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthPlan.Quote.Infrastructure.Implementation
{
    /// <summary>
    /// Entity Framework configuration for the Coverage entity.
    /// Defines table structure, constraints, and relationships.
    /// </summary>
    internal class CoverageMap : IEntityTypeConfiguration<AgeRange>, IEntityTypeConfiguration<Coverage>
    {
        /// <summary>
        /// Configures the Coverage entity for Entity Framework.
        /// </summary>
        /// <param name="builder">Entity type builder for Coverage</param>
        public void Configure(EntityTypeBuilder<Coverage> builder)
        {
            builder.ToTable("Coverages");
            

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Description)
                .HasMaxLength(300);

            builder.Property(e => e.CoverageType)
                .IsRequired()
                .HasMaxLength(50);

            // Create index on coverage type for efficient filtering
            builder.HasIndex(e => e.CoverageType)
                .HasDatabaseName("IX_Coverages_CoverageType");
        }
    }
}