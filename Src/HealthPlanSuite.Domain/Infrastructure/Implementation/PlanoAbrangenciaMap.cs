using Foundation.Base.Infrastructure.Data;
using HealthPlanSuite.Domain.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthPlanSuite.Infrastructure.Implementation
{
    public class PlanoAbrangenciaMap : EntityConfiguration<PlanoAbrangencia>
    {
        public override void Configure(EntityTypeBuilder<PlanoAbrangencia> builder)
        {
            builder.ToTable("PlanosAbrangencia");

            builder.HasKey(pa => pa.Id);

            builder.Property(pa => pa.PlanoId)
                .IsRequired();

            builder.Property(pa => pa.EstabelecimentoId)
                .IsRequired();

            builder.Property(pa => pa.CreatedAt)
                .IsRequired();

            builder.Property(pa => pa.UpdatedAt)
                .IsRequired();

            // Unique constraint
            builder.HasIndex(pa => new { pa.PlanoId, pa.EstabelecimentoId })
                .IsUnique()
                .HasDatabaseName("IX_PlanosAbrangencia_PlanoEstabelecimento");

            // Additional indexes
            builder.HasIndex(pa => pa.PlanoId)
                .HasDatabaseName("IX_PlanosAbrangencia_PlanoId");

            builder.HasIndex(pa => pa.EstabelecimentoId)
                .HasDatabaseName("IX_PlanosAbrangencia_EstabelecimentoId");

            // Relationships
            builder.HasOne(pa => pa.Plano)
                .WithMany(p => p.PlanosAbrangencia)
                .HasForeignKey(pa => pa.PlanoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pa => pa.EstabelecimentoSaude)
                .WithMany(es => es.PlanosAbrangencia)
                .HasForeignKey(pa => pa.EstabelecimentoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}