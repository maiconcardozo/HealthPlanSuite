using Foundation.Base.Infrastructure.Data;
using HealthPlanSuite.Domain.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthPlanSuite.Infrastructure.Implementation
{
    public class FaixaEtariaMap : EntityConfiguration<FaixaEtaria>
    {
        public override void Configure(EntityTypeBuilder<FaixaEtaria> builder)
        {
            builder.ToTable("FaixasEtaria");

            builder.HasKey(fe => fe.Id);

            builder.Property(fe => fe.Descricao)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(fe => fe.IdadeMin)
                .IsRequired();

            builder.Property(fe => fe.IdadeMax)
                .IsRequired();

            builder.Property(fe => fe.CreatedAt)
                .IsRequired();

            builder.Property(fe => fe.UpdatedAt)
                .IsRequired();

            // Indexes
            builder.HasIndex(fe => fe.Descricao)
                .IsUnique()
                .HasDatabaseName("IX_FaixasEtaria_Descricao");

            builder.HasIndex(fe => new { fe.IdadeMin, fe.IdadeMax })
                .HasDatabaseName("IX_FaixasEtaria_Idades");

            // Relationships
            builder.HasMany(fe => fe.TabelasPreco)
                .WithOne(tp => tp.FaixaEtaria)
                .HasForeignKey(tp => tp.FaixaEtariaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}