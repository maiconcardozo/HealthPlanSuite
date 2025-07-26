using Microsoft.EntityFrameworkCore;
using HealthPlanSuite.Domain.Implementation;

namespace HealthPlanSuite.Infrastructure.Interface
{
    public interface IHealthPlanSuiteContext : IDisposable
    {
        DbSet<Operadora> Operadoras { get; set; }
        DbSet<TipoPlano> TiposPlano { get; set; }
        DbSet<FaixaEtaria> FaixasEtaria { get; set; }
        DbSet<EstabelecimentoSaude> EstabelecimentosSaude { get; set; }
        DbSet<Plano> Planos { get; set; }
        DbSet<TabelaPreco> TabelasPreco { get; set; }
        DbSet<Reajuste> Reajustes { get; set; }
        DbSet<PlanoAbrangencia> PlanosAbrangencia { get; set; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        DbSet<T> Set<T>() where T : class;
    }
}