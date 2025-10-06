using HealthPlan.Quote.Domain.Implementation;

namespace HealthPlan.Quote.Services.Interface
{
    /// <summary>
    /// Service interface for ProcedureCoparticipation business operations.
    /// Provides business logic layer for ProcedureCoparticipation management.
    /// </summary>
    public interface IProcedureCoparticipationService
    {
        IEnumerable<ProcedureCoparticipation> GetAllActiveCoparticipacaoProcedimento();
        ProcedureCoparticipation? GetById(int id);
        void AddCoparticipacaoProcedimento(ProcedureCoparticipation coparticipacaoProcedimento);
        void UpdateCoparticipacaoProcedimento(ProcedureCoparticipation coparticipacaoProcedimento);
        void DeleteCoparticipacaoProcedimento(int id);
    }
}