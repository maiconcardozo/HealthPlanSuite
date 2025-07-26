using Foundation.Base.Infrastructure.Data;
using HealthPlanSuite.Domain.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthPlanSuite.Infrastructure.Implementation
{
    public class ReajusteMap : EntityConfiguration<Reajuste>
    {
        public override void Configure(EntityTypeBuilder<Reajuste> builder)
        {
            builder.ToTable("Reajustes");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.PlanoId)
                .IsRequired();

            builder.Property(r => r.Percentual)
                .IsRequired()
                .HasColumnType("decimal(5,2)");

            builder.Property(r => r.DataReajuste)
                .IsRequired();

            builder.Property(r => r.TipoReajuste)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(r => r.Observacao)
                .HasMaxLength(500);

            builder.Property(r => r.CreatedAt)
                .IsRequired();

            builder.Property(r => r.UpdatedAt)
                .IsRequired();

            // Indexes
            builder.HasIndex(r => r.PlanoId)
                .HasDatabaseName("IX_Reajustes_PlanoId");

            builder.HasIndex(r => r.DataReajuste)
                .HasDatabaseName("IX_Reajustes_DataReajuste");

            builder.HasIndex(r => r.TipoReajuste)
                .HasDatabaseName("IX_Reajustes_TipoReajuste");

            builder.HasIndex(r => new { r.PlanoId, r.DataReajuste })
                .HasDatabaseName("IX_Reajustes_PlanoData");

            // Relationships
            builder.HasOne(r => r.Plano)
                .WithMany(p => p.Reajustes)
                .HasForeignKey(r => r.PlanoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}