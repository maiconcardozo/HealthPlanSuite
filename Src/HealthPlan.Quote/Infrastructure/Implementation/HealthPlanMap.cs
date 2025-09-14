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
                .HasColumnName("IdPlanoSaude")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.IdEmpresa)
                .HasColumnName("IdEmpresa")
                .IsRequired();

            builder.Property(e => e.IdAcomodacao)
                .HasColumnName("IdAcomodacao")
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

            // Create index on IdEmpresa for efficient filtering
            builder.HasIndex(e => e.IdEmpresa)
                .HasDatabaseName("IX_HealthPlans_IdEmpresa");

            // Create index on IdAcomodacao for efficient filtering
            builder.HasIndex(e => e.IdAcomodacao)
                .HasDatabaseName("IX_HealthPlans_IdAcomodacao");

            // Foreign key relationships
            builder.HasOne<Company>()
                .WithMany()
                .HasForeignKey(e => e.IdEmpresa)
                .OnDelete(DeleteBehavior.Restrict);
                
            builder.HasOne<Accommodation>()
                .WithMany()
                .HasForeignKey(e => e.IdAcomodacao)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}