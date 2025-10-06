using HealthPlan.Quote.Domain.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthPlan.Quote.Infrastructure.Implementation
{
    /// <summary>
    /// Entity Framework configuration for the PlanPriceRange entity.
    /// Defines table structure, constraints, and relationships.
    /// </summary>
    internal class PlanPriceRangeMap : IEntityTypeConfiguration<PlanPriceRange>
    {
        /// <summary>
        /// Configures the PlanPriceRange entity for Entity Framework.
        /// </summary>
        /// <param name="builder">Entity type builder for PlanPriceRange</param>
        public void Configure(EntityTypeBuilder<PlanPriceRange> builder)
        {
            builder.ToTable("PlanPriceRange");

            // Primary key
            builder.HasKey(x => x.Id);
            
            // Properties configuration
            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.HealthPlanId)
                .IsRequired()
                .HasColumnName("HealthPlanId");

            builder.Property(e => e.AgeRangeId)
                .IsRequired()
                .HasColumnName("AgeRangeId");

            builder.Property(e => e.ContractType)
                .IsRequired()
                .HasColumnName("TipoContratacao")
                .HasMaxLength(50);

            builder.Property(e => e.CoparticipationType)
                .IsRequired()
                .HasColumnName("TipoCoparticipacao")
                .HasMaxLength(50);

            builder.Property(e => e.OriginalValue)
                .IsRequired()
                .HasColumnName("ValorOriginal")
                .HasColumnType("decimal(18,2)");

            builder.Property(e => e.DiscountValue)
                .IsRequired()
                .HasColumnName("ValorDesconto")
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

            // Foreign key relationships
            builder.HasOne(e => e.HealthPlan)
                .WithMany()
                .HasForeignKey(e => e.HealthPlanId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.AgeRange)
                .WithMany()
                .HasForeignKey(e => e.AgeRangeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Create indexes for efficient lookup
            builder.HasIndex(e => new { e.HealthPlanId, e.AgeRangeId, e.ContractType, e.CoparticipationType, e.ValidityStart, e.ValidityEnd })
                .HasDatabaseName("IX_PrecoPlanoFaixa_Lookup");

            builder.HasIndex(e => new { e.ValidityStart, e.ValidityEnd })
                .HasDatabaseName("IX_PrecoPlanoFaixa_Validity");
        }
    }
}