using HealthPlan.Quote.Domain.Implementation;

namespace HealthPlan.Quote.Services.Interface
{
    /// <summary>
    /// Service interface for PrecoPlanoFaixa business operations.
    /// Provides business logic layer for PrecoPlanoFaixa management.
    /// </summary>
    public interface IPrecoPlanoFaixaService
    {
        IEnumerable<PrecoPlanoFaixa> GetAllActivePrecoPlanoFaixa();
        PrecoPlanoFaixa? GetById(int id);
        void AddPrecoPlanoFaixa(PrecoPlanoFaixa precoPlanoFaixa);
        void UpdatePrecoPlanoFaixa(PrecoPlanoFaixa precoPlanoFaixa);
        void DeletePrecoPlanoFaixa(int id);
    }
}