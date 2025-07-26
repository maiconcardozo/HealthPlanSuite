using Foundation.Base.Infrastructure.Data;
using HealthPlanSuite.Domain.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthPlanSuite.Infrastructure.Implementation
{
    public class TipoPlanoMap : EntityConfiguration<TipoPlano>
    {
        public override void Configure(EntityTypeBuilder<TipoPlano> builder)
        {
            builder.ToTable("TiposPlano");

            builder.HasKey(tp => tp.Id);

            builder.Property(tp => tp.Descricao)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(tp => tp.RegulacaoANS)
                .HasMaxLength(500);

            builder.Property(tp => tp.CreatedAt)
                .IsRequired();

            builder.Property(tp => tp.UpdatedAt)
                .IsRequired();

            // Indexes
            builder.HasIndex(tp => tp.Descricao)
                .IsUnique()
                .HasDatabaseName("IX_TiposPlano_Descricao");

            // Relationships
            builder.HasMany(tp => tp.Planos)
                .WithOne(p => p.TipoPlano)
                .HasForeignKey(p => p.TipoPlanoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}