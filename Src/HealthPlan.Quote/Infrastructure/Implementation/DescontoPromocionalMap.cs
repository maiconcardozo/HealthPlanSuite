using HealthPlan.Quote.Domain.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthPlan.Quote.Infrastructure.Implementation
{
    /// <summary>
    /// Entity Framework configuration for the DescontoPromocional entity.
    /// Defines table structure, constraints, and relationships.
    /// </summary>
    internal class DescontoPromocionalMap : IEntityTypeConfiguration<DescontoPromocional>
    {
        /// <summary>
        /// Configures the DescontoPromocional entity for Entity Framework.
        /// </summary>
        /// <param name="builder">Entity type builder for DescontoPromocional</param>
        public void Configure(EntityTypeBuilder<DescontoPromocional> builder)
        {
            builder.ToTable("DescontoPromocional");

            // Primary key
            builder.HasKey(x => x.Id);
            
            // Properties configuration
            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.HealthPlanId)
                .IsRequired()
                .HasColumnName("HealthPlanId");

            builder.Property(e => e.PercentualDesconto)
                .IsRequired()
                .HasColumnName("PercentualDesconto")
                .HasColumnType("decimal(5,2)");

            builder.Property(e => e.ValidadeInicio)
                .IsRequired()
                .HasColumnName("ValidadeInicio")
                .HasColumnType("datetime");

            builder.Property(e => e.ValidadeFim)
                .IsRequired()
                .HasColumnName("ValidadeFim")
                .HasColumnType("datetime");

            builder.Property(e => e.Observacao)
                .HasColumnName("Observacao")
                .HasMaxLength(500);

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
            builder.HasIndex(e => new { e.HealthPlanId, e.ValidadeInicio, e.ValidadeFim })
                .HasDatabaseName("IX_DescontoPromocional_HealthPlan_Validity");
        }
    }
}