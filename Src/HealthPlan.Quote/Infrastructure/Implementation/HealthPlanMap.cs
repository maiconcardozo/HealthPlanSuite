using HealthPlan.Quote.Domain.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthPlan.Quote.Infrastructure.Implementation
{
    /// <summary>
    /// Entity Framework configuration for the HealthPlan entity.
    /// Defines table structure, constraints, and relationships.
    /// </summary>
    internal class HealthPlanMap : IEntityTypeConfiguration<Domain.Implementation.HealthPlan>
    {
        /// <summary>
        /// Configures the HealthPlan entity for Entity Framework.
        /// </summary>
        /// <param name="builder">Entity type builder for HealthPlan</param>
        public void Configure(EntityTypeBuilder<Domain.Implementation.HealthPlan> builder)
        {
            builder.ToTable("HealthPlans");
            
            // Primary key
            builder.HasKey(x => x.Id);
            
            // Properties configuration
            builder.Property(x => x.Id)
                .HasColumnName("IdHealthPlan")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.IdCompany)
                .HasColumnName("IdCompany")
                .IsRequired();

            builder.Property(e => e.IdAccommodation)
                .HasColumnName("IdAccommodation")
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

            // Create index on IdCompany for efficient filtering
            builder.HasIndex(e => e.IdCompany)
                .HasDatabaseName("IX_HealthPlans_IdCompany");

            // Create index on IdAccommodation for efficient filtering
            builder.HasIndex(e => e.IdAccommodation)
                .HasDatabaseName("IX_HealthPlans_IdAccommodation");

            // Foreign key relationships
            builder.HasOne<Company>()
                .WithMany()
                .HasForeignKey(e => e.IdCompany)
                .OnDelete(DeleteBehavior.Restrict);
                
            builder.HasOne<Accommodation>()
                .WithMany()
                .HasForeignKey(e => e.IdAccommodation)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}