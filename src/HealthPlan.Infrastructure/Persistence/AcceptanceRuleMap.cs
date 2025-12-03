using HealthPlan.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthPlan.Infrastructure.Persistence
{
    /// <summary>
    /// Entity Framework configuration for the AcceptanceRule entity.
    /// Defines table structure, constraints, and relationships.
    /// </summary>
    internal class AcceptanceRuleMap : IEntityTypeConfiguration<AcceptanceRule>
    {
        /// <summary>
        /// Configures the AcceptanceRule entity for Entity Framework.
        /// </summary>
        /// <param name="builder">Entity type builder for AcceptanceRule</param>
        public void Configure(EntityTypeBuilder<AcceptanceRule> builder)
        {
            builder.ToTable("AcceptanceRules");
            
            // Primary key
            builder.HasKey(x => x.Id);
            
            // Properties configuration
            builder.Property(x => x.Id)
                .HasColumnName("IdAcceptanceRule")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.IdHealthPlan)
                .HasColumnName("IdHealthPlan")
                .IsRequired();

            builder.Property(e => e.RuleType)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Operator)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(e => e.MinValue)
                .HasMaxLength(255);

            builder.Property(e => e.MaxValue)
                .HasMaxLength(255);

            builder.Property(e => e.ValuesList)
                .HasColumnType("text");

            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(e => e.RejectionMessage)
                .HasMaxLength(500);

            builder.Property(e => e.IsMandatory)
                .HasDefaultValue(true);

            // Create indexes for efficient filtering
            builder.HasIndex(e => e.IdHealthPlan)
                .HasDatabaseName("IX_AcceptanceRules_IdHealthPlan");

            builder.HasIndex(e => e.RuleType)
                .HasDatabaseName("IX_AcceptanceRules_RuleType");

            // Foreign key relationship
            builder.HasOne<Domain.Entities.HealthPlan>()
                .WithMany()
                .HasForeignKey(e => e.IdHealthPlan)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}