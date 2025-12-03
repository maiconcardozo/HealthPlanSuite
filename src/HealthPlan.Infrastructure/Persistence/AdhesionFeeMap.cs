using HealthPlan.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthPlan.Infrastructure.Persistence
{
    /// <summary>
    /// Entity Framework configuration for the AdhesionFee entity.
    /// Defines table structure, constraints, and relationships.
    /// </summary>
    internal class AdhesionFeeMap : IEntityTypeConfiguration<AdhesionFee>
    {
        /// <summary>
        /// Configures the AdhesionFee entity for Entity Framework.
        /// </summary>
        /// <param name="builder">Entity type builder for AdhesionFee</param>
        public void Configure(EntityTypeBuilder<AdhesionFee> builder)
        {
            builder.ToTable("AdhesionFee");

            // Primary key
            builder.HasKey(x => x.Id);
            
            // Properties configuration
            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.HealthPlanId)
                .IsRequired()
                .HasColumnName("HealthPlanId");

            builder.Property(e => e.Value)
                .IsRequired()
                .HasColumnName("Valor")
                .HasColumnType("decimal(18,2)");

            builder.Property(e => e.ValidityStart)
                .IsRequired()
                .HasColumnName("ValidadeInicio")
                .HasColumnType("datetime");

            builder.Property(e => e.ValidityEnd)
                .IsRequired()
                .HasColumnName("ValidadeFim")
                .HasColumnType("datetime");

            // Base entity properties
            builder.Property(x => x.IsActive)
                .HasColumnName("IsActive")
                .HasDefaultValue(true);

            builder.Property(x => x.DtCreated)
                .HasColumnName("DtCreated")
                .HasDefaultValueSql("NOW()");

            builder.Property(x => x.DtUpdated)
                .HasColumnName("DtUpdated");

            builder.Property(x => x.DtDeleted)
                .HasColumnName("DtDeleted");

            builder.Property(x => x.DeletedBy)
                .HasColumnName("DeletedBy")
                .HasMaxLength(100);

            // Foreign key relationship
            builder.HasOne(e => e.HealthPlan)
                .WithMany()
                .HasForeignKey(e => e.HealthPlanId)
                .OnDelete(DeleteBehavior.Restrict);

            // Create index on health plan and validity dates for efficient lookup
            builder.HasIndex(e => new { e.HealthPlanId, e.ValidityStart, e.ValidityEnd })
                .HasDatabaseName("IX_TaxaAdesao_HealthPlan_Validity");
        }
    }
}