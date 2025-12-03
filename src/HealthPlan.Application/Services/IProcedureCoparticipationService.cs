using HealthPlan.Domain.Entities;

namespace HealthPlan.Application.Services
{
    /// <summary>
    /// Service interface for ProcedureCoparticipation business operations.
    /// Provides business logic layer for ProcedureCoparticipation management.
    /// </summary>
    public interface IProcedureCoparticipationService
    {
        IEnumerable<ProcedureCoparticipation> GetAllActiveProcedureCoparticipations();
        ProcedureCoparticipation? GetById(int id);
        void AddProcedureCoparticipation(ProcedureCoparticipation procedureCoparticipation);
        void UpdateProcedureCoparticipation(ProcedureCoparticipation procedureCoparticipation);
        void DeleteProcedureCoparticipation(int id);
    }
}