using HealthPlanSuite.Core.Domain.Implementation;
using Microsoft.EntityFrameworkCore;

namespace HealthPlanSuite.Core.Infrastructure
{
    public class HealthPlanSuiteDbContext : DbContext
    {
        public HealthPlanSuiteDbContext(DbContextOptions<HealthPlanSuiteDbContext> options)
            : base(options)
        {
        }

        public DbSet<Operadora> Operadoras { get; set; }
        public DbSet<TipoPlano> TiposPlano { get; set; }
        public DbSet<Plano> Planos { get; set; }
        public DbSet<FaixaEtaria> FaixasEtaria { get; set; }
        public DbSet<TabelaPreco> TabelasPreco { get; set; }
        public DbSet<Reajuste> Reajustes { get; set; }
        public DbSet<EstabelecimentoSaude> EstabelecimentosSaude { get; set; }
        public DbSet<PlanoAbrangencia> PlanosAbrangencia { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships and constraints
            modelBuilder.Entity<Plano>()
                .HasOne(p => p.Operadora)
                .WithMany(o => o.Planos)
                .HasForeignKey(p => p.OperadoraId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Plano>()
                .HasOne(p => p.TipoPlano)
                .WithMany(tp => tp.Planos)
                .HasForeignKey(p => p.TipoPlanoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TabelaPreco>()
                .HasOne(tp => tp.Plano)
                .WithMany(p => p.TabelasPreco)
                .HasForeignKey(tp => tp.PlanoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TabelaPreco>()
                .HasOne(tp => tp.FaixaEtaria)
                .WithMany(fe => fe.TabelasPreco)
                .HasForeignKey(tp => tp.FaixaEtariaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reajuste>()
                .HasOne(r => r.Plano)
                .WithMany(p => p.Reajustes)
                .HasForeignKey(r => r.PlanoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PlanoAbrangencia>()
                .HasOne(pa => pa.Plano)
                .WithMany(p => p.PlanoAbrangencias)
                .HasForeignKey(pa => pa.PlanoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PlanoAbrangencia>()
                .HasOne(pa => pa.EstabelecimentoSaude)
                .WithMany(es => es.PlanoAbrangencias)
                .HasForeignKey(pa => pa.EstabelecimentoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Add unique constraints
            modelBuilder.Entity<Operadora>()
                .HasIndex(o => o.CNPJ)
                .IsUnique();

            modelBuilder.Entity<PlanoAbrangencia>()
                .HasIndex(pa => new { pa.PlanoId, pa.EstabelecimentoId })
                .IsUnique();

            modelBuilder.Entity<TabelaPreco>()
                .HasIndex(tp => new { tp.PlanoId, tp.FaixaEtariaId, tp.DataInicioVigencia })
                .IsUnique();
        }
    }
}