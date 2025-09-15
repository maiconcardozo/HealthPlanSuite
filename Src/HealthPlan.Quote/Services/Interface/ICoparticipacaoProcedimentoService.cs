using HealthPlan.Quote.Domain.Implementation;

namespace HealthPlan.Quote.Services.Interface
{
    /// <summary>
    /// Service interface for CoparticipacaoProcedimento business operations.
    /// Provides business logic layer for CoparticipacaoProcedimento management.
    /// </summary>
    public interface ICoparticipacaoProcedimentoService
    {
        IEnumerable<CoparticipacaoProcedimento> GetAllActiveCoparticipacaoProcedimento();
        CoparticipacaoProcedimento? GetById(int id);
        void AddCoparticipacaoProcedimento(CoparticipacaoProcedimento coparticipacaoProcedimento);
        void UpdateCoparticipacaoProcedimento(CoparticipacaoProcedimento coparticipacaoProcedimento);
        void DeleteCoparticipacaoProcedimento(int id);
    }
}