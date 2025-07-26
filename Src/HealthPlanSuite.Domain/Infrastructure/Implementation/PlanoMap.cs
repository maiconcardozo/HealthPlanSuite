using Foundation.Base.Infrastructure.Data;
using HealthPlanSuite.Domain.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthPlanSuite.Infrastructure.Implementation
{
    public class PlanoMap : EntityConfiguration<Plano>
    {
        public override void Configure(EntityTypeBuilder<Plano> builder)
        {
            builder.ToTable("Planos");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.OperadoraId)
                .IsRequired();

            builder.Property(p => p.TipoPlanoId)
                .IsRequired();

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(p => p.Cobertura)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(p => p.PossuiCoparticipacao)
                .IsRequired();

            builder.Property(p => p.CreatedAt)
                .IsRequired();

            builder.Property(p => p.UpdatedAt)
                .IsRequired();

            // Indexes
            builder.HasIndex(p => p.Nome)
                .HasDatabaseName("IX_Planos_Nome");

            builder.HasIndex(p => p.OperadoraId)
                .HasDatabaseName("IX_Planos_OperadoraId");

            builder.HasIndex(p => p.TipoPlanoId)
                .HasDatabaseName("IX_Planos_TipoPlanoId");

            // Relationships
            builder.HasOne(p => p.Operadora)
                .WithMany(o => o.Planos)
                .HasForeignKey(p => p.OperadoraId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.TipoPlano)
                .WithMany(tp => tp.Planos)
                .HasForeignKey(p => p.TipoPlanoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.TabelasPreco)
                .WithOne(tp => tp.Plano)
                .HasForeignKey(tp => tp.PlanoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Reajustes)
                .WithOne(r => r.Plano)
                .HasForeignKey(r => r.PlanoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.PlanosAbrangencia)
                .WithOne(pa => pa.Plano)
                .HasForeignKey(pa => pa.PlanoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}