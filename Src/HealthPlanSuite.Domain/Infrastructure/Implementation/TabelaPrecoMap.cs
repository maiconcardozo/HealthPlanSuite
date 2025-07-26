using Foundation.Base.Infrastructure.Data;
using HealthPlanSuite.Domain.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthPlanSuite.Infrastructure.Implementation
{
    public class TabelaPrecoMap : EntityConfiguration<TabelaPreco>
    {
        public override void Configure(EntityTypeBuilder<TabelaPreco> builder)
        {
            builder.ToTable("TabelasPreco");

            builder.HasKey(tp => tp.Id);

            builder.Property(tp => tp.PlanoId)
                .IsRequired();

            builder.Property(tp => tp.FaixaEtariaId)
                .IsRequired();

            builder.Property(tp => tp.Mensalidade)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(tp => tp.ValorCoparticipacao)
                .HasColumnType("decimal(18,2)");

            builder.Property(tp => tp.DataInicioVigencia)
                .IsRequired();

            builder.Property(tp => tp.DataFimVigencia);

            builder.Property(tp => tp.CreatedAt)
                .IsRequired();

            builder.Property(tp => tp.UpdatedAt)
                .IsRequired();

            // Indexes
            builder.HasIndex(tp => tp.PlanoId)
                .HasDatabaseName("IX_TabelasPreco_PlanoId");

            builder.HasIndex(tp => tp.FaixaEtariaId)
                .HasDatabaseName("IX_TabelasPreco_FaixaEtariaId");

            builder.HasIndex(tp => new { tp.DataInicioVigencia, tp.DataFimVigencia })
                .HasDatabaseName("IX_TabelasPreco_Vigencia");

            builder.HasIndex(tp => new { tp.PlanoId, tp.FaixaEtariaId, tp.DataInicioVigencia })
                .HasDatabaseName("IX_TabelasPreco_PlanoFaixaVigencia");

            // Relationships
            builder.HasOne(tp => tp.Plano)
                .WithMany(p => p.TabelasPreco)
                .HasForeignKey(tp => tp.PlanoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(tp => tp.FaixaEtaria)
                .WithMany(fe => fe.TabelasPreco)
                .HasForeignKey(tp => tp.FaixaEtariaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}