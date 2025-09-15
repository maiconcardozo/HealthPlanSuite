using HealthPlan.Quote.Domain.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthPlan.Quote.Infrastructure.Implementation
{
    /// <summary>
    /// Entity Framework configuration for the CoparticipacaoProcedimento entity.
    /// Defines table structure, constraints, and relationships.
    /// </summary>
    internal class CoparticipacaoProcedimentoMap : IEntityTypeConfiguration<CoparticipacaoProcedimento>
    {
        /// <summary>
        /// Configures the CoparticipacaoProcedimento entity for Entity Framework.
        /// </summary>
        /// <param name="builder">Entity type builder for CoparticipacaoProcedimento</param>
        public void Configure(EntityTypeBuilder<CoparticipacaoProcedimento> builder)
        {
            builder.ToTable("CoparticipacaoProcedimento");

            // Primary key
            builder.HasKey(x => x.Id);
            
            // Properties configuration
            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.HealthPlanId)
                .IsRequired()
                .HasColumnName("HealthPlanId");

            builder.Property(e => e.TipoCoparticipacao)
                .IsRequired()
                .HasColumnName("TipoCoparticipacao")
                .HasMaxLength(50);

            builder.Property(e => e.Procedimento)
                .IsRequired()
                .HasColumnName("Procedimento")
                .HasMaxLength(200);

            builder.Property(e => e.Valor)
                .IsRequired()
                .HasColumnName("Valor")
                .HasColumnType("decimal(18,2)");

            builder.Property(e => e.Limite)
                .HasColumnName("Limite")
                .HasColumnType("decimal(18,2)");

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

            // Create indexes for efficient lookup
            builder.HasIndex(e => new { e.HealthPlanId, e.TipoCoparticipacao })
                .HasDatabaseName("IX_CoparticipacaoProcedimento_HealthPlan_Tipo");

            builder.HasIndex(e => new { e.HealthPlanId, e.Procedimento })
                .HasDatabaseName("IX_CoparticipacaoProcedimento_HealthPlan_Procedimento");
        }
    }
}