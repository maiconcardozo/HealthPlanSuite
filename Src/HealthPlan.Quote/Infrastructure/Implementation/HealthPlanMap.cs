using HealthPlan.Quote.Domain.Implementation;
using Foundation.Base.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthPlan.Quote.Infrastructure.Implementation
{
    /// <summary>
    /// Entity Framework configuration for the HealthPlan entity.
    /// Defines table structure, constraints, and relationships.
    /// </summary>
    internal class HealthPlanMap : EntityMap<Domain.Implementation.HealthPlan>, IEntityTypeConfiguration<Domain.Implementation.HealthPlan>
    {
        /// <summary>
        /// Configures the HealthPlan entity for Entity Framework.
        /// </summary>
        /// <param name="builder">Entity type builder for HealthPlan</param>
        public override void Configure(EntityTypeBuilder<Domain.Implementation.HealthPlan> builder)
        {
            builder.ToTable("HealthPlans");
            base.Configure(builder);

            builder.Property(e => e.CompanyId)
                .IsRequired();

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Description)
                .HasMaxLength(500);

            builder.Property(e => e.PlanType)
                .IsRequired()
                .HasMaxLength(50);

            // Create unique index on Code for business logic enforcement
            builder.HasIndex(e => e.Code)
                .IsUnique()
                .HasDatabaseName("IX_HealthPlans_Code_Unique");

            // Create index on CompanyId for efficient filtering
            builder.HasIndex(e => e.CompanyId)
                .HasDatabaseName("IX_HealthPlans_CompanyId");

            // Foreign key relationship
            builder.HasOne<Company>()
                .WithMany()
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}