using Foundation.Base.Infrastructure.Data;
using HealthPlanSuite.Domain.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthPlanSuite.Infrastructure.Implementation
{
    public class OperadoraMap : EntityConfiguration<Operadora>
    {
        public override void Configure(EntityTypeBuilder<Operadora> builder)
        {
            builder.ToTable("Operadoras");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(o => o.CNPJ)
                .IsRequired()
                .HasMaxLength(18); // 14 digits + formatting

            builder.Property(o => o.Site)
                .HasMaxLength(255);

            builder.Property(o => o.Telefone)
                .HasMaxLength(20);

            builder.Property(o => o.CreatedAt)
                .IsRequired();

            builder.Property(o => o.UpdatedAt)
                .IsRequired();

            // Indexes
            builder.HasIndex(o => o.CNPJ)
                .IsUnique()
                .HasDatabaseName("IX_Operadoras_CNPJ");

            builder.HasIndex(o => o.Nome)
                .HasDatabaseName("IX_Operadoras_Nome");

            // Relationships
            builder.HasMany(o => o.Planos)
                .WithOne(p => p.Operadora)
                .HasForeignKey(p => p.OperadoraId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}