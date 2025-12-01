using HealthPlan.Quote.Domain.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthPlan.Quote.Infrastructure.Implementation
{
    /// <summary>
    /// Entity Framework configuration for the PlanCoverage entity.
    /// Defines table structure, constraints, and relationships.
    /// </summary>
    internal class PlanCoverageMap : IEntityTypeConfiguration<PlanCoverage>
    {
        /// <summary>
        /// Configures the PlanCoverage entity for Entity Framework.
        /// </summary>
        /// <param name="builder">Entity type builder for PlanCoverage</param>
        public void Configure(EntityTypeBuilder<PlanCoverage> builder)
        {
            builder.ToTable("PlanCoverages");
            
            // Primary key
            builder.HasKey(x => x.Id);
            
            // Properties configuration
            builder.Property(x => x.Id)
                .HasColumnName("IdPlanCoverage")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.IdHealthPlan)
                .HasColumnName("IdHealthPlan")
                .IsRequired();

            builder.Property(e => e.IdCoverage)
                .HasColumnName("IdCoverage")
                .IsRequired();

            builder.Property(e => e.PremiumValue)
                .HasColumnType("decimal(10,2)")
                .HasDefaultValue(0.00m);

            builder.Property(e => e.IsIncluded)
                .HasDefaultValue(true);

            // Create unique constraint on PlanId + CoverageId
            builder.HasIndex(e => new { e.IdHealthPlan, e.IdCoverage })
                .IsUnique()
                .HasDatabaseName("UK_PlanCoverage_HealthPlan_Coverage");

            // Create indexes for efficient filtering
            builder.HasIndex(e => e.IdHealthPlan)
                .HasDatabaseName("IX_PlanCoverages_IdHealthPlan");

            builder.HasIndex(e => e.IdCoverage)
                .HasDatabaseName("IX_PlanCoverages_IdCoverage");

            // Foreign key relationships
            builder.HasOne<Domain.Implementation.HealthPlan>()
                .WithMany()
                .HasForeignKey(e => e.IdHealthPlan)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Coverage>()
                .WithMany()
                .HasForeignKey(e => e.IdCoverage)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}