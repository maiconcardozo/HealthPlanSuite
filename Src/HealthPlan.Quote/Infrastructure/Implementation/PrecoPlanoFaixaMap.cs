using HealthPlan.Quote.Domain.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthPlan.Quote.Infrastructure.Implementation
{
    /// <summary>
    /// Entity Framework configuration for the PrecoPlanoFaixa entity.
    /// Defines table structure, constraints, and relationships.
    /// </summary>
    internal class PrecoPlanoFaixaMap : IEntityTypeConfiguration<PrecoPlanoFaixa>
    {
        /// <summary>
        /// Configures the PrecoPlanoFaixa entity for Entity Framework.
        /// </summary>
        /// <param name="builder">Entity type builder for PrecoPlanoFaixa</param>
        public void Configure(EntityTypeBuilder<PrecoPlanoFaixa> builder)
        {
            builder.ToTable("PrecoPlanoFaixa");

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

            builder.Property(e => e.TipoContratacao)
                .IsRequired()
                .HasColumnName("TipoContratacao")
                .HasMaxLength(50);

            builder.Property(e => e.TipoCoparticipacao)
                .IsRequired()
                .HasColumnName("TipoCoparticipacao")
                .HasMaxLength(50);

            builder.Property(e => e.ValorOriginal)
                .IsRequired()
                .HasColumnName("ValorOriginal")
                .HasColumnType("decimal(18,2)");

            builder.Property(e => e.ValorDesconto)
                .IsRequired()
                .HasColumnName("ValorDesconto")
                .HasColumnType("decimal(18,2)");

            builder.Property(e => e.ValidadeInicio)
                .IsRequired()
                .HasColumnName("ValidadeInicio")
                .HasColumnType("datetime");

            builder.Property(e => e.ValidadeFim)
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
            builder.HasIndex(e => new { e.HealthPlanId, e.AgeRangeId, e.TipoContratacao, e.TipoCoparticipacao, e.ValidadeInicio, e.ValidadeFim })
                .HasDatabaseName("IX_PrecoPlanoFaixa_Lookup");

            builder.HasIndex(e => new { e.ValidadeInicio, e.ValidadeFim })
                .HasDatabaseName("IX_PrecoPlanoFaixa_Validity");
        }
    }
}