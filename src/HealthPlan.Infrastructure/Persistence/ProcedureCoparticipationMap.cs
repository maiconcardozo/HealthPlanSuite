using HealthPlan.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthPlan.Infrastructure.Persistence
{
    /// <summary>
    /// Entity Framework configuration for the ProcedureCoparticipation entity.
    /// Defines table structure, constraints, and relationships.
    /// </summary>
    internal class ProcedureCoparticipationMap : IEntityTypeConfiguration<ProcedureCoparticipation>
    {
        /// <summary>
        /// Configures the ProcedureCoparticipation entity for Entity Framework.
        /// </summary>
        /// <param name="builder">Entity type builder for ProcedureCoparticipation</param>
        public void Configure(EntityTypeBuilder<ProcedureCoparticipation> builder)
        {
            builder.ToTable("ProcedureCoparticipation");

            // Primary key
            builder.HasKey(x => x.Id);
            
            // Properties configuration
            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.HealthPlanId)
                .IsRequired()
                .HasColumnName("HealthPlanId");

            builder.Property(e => e.CoparticipationType)
                .IsRequired()
                .HasColumnName("TipoCoparticipacao")
                .HasMaxLength(50);

            builder.Property(e => e.Procedure)
                .IsRequired()
                .HasColumnName("Procedimento")
                .HasMaxLength(200);

            builder.Property(e => e.Value)
                .IsRequired()
                .HasColumnName("Valor")
                .HasColumnType("decimal(18,2)");

            builder.Property(e => e.Limit)
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
            builder.HasIndex(e => new { e.HealthPlanId, e.CoparticipationType })
                .HasDatabaseName("IX_CoparticipacaoProcedimento_HealthPlan_Tipo");

            builder.HasIndex(e => new { e.HealthPlanId, e.Procedure })
                .HasDatabaseName("IX_CoparticipacaoProcedimento_HealthPlan_Procedimento");
        }
    }
}