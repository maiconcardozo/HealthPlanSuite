using Foundation.Base.Infrastructure.Data;
using HealthPlanSuite.Domain.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthPlanSuite.Infrastructure.Implementation
{
    public class EstabelecimentoSaudeMap : EntityConfiguration<EstabelecimentoSaude>
    {
        public override void Configure(EntityTypeBuilder<EstabelecimentoSaude> builder)
        {
            builder.ToTable("EstabelecimentosSaude");

            builder.HasKey(es => es.Id);

            builder.Property(es => es.Nome)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(es => es.Tipo)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(es => es.Endereco)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(es => es.Cidade)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(es => es.Estado)
                .IsRequired()
                .HasMaxLength(2);

            builder.Property(es => es.CreatedAt)
                .IsRequired();

            builder.Property(es => es.UpdatedAt)
                .IsRequired();

            // Indexes
            builder.HasIndex(es => es.Nome)
                .HasDatabaseName("IX_EstabelecimentosSaude_Nome");

            builder.HasIndex(es => es.Tipo)
                .HasDatabaseName("IX_EstabelecimentosSaude_Tipo");

            builder.HasIndex(es => es.Cidade)
                .HasDatabaseName("IX_EstabelecimentosSaude_Cidade");

            builder.HasIndex(es => es.Estado)
                .HasDatabaseName("IX_EstabelecimentosSaude_Estado");

            // Relationships
            builder.HasMany(es => es.PlanosAbrangencia)
                .WithOne(pa => pa.EstabelecimentoSaude)
                .HasForeignKey(pa => pa.EstabelecimentoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}