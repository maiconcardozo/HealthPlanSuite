using HealthPlan.Quote.Domain.Implementation;
using HealthPlan.Quote.Repository.Interface;
using HealthPlan.Quote.Services.Interface;

namespace HealthPlan.Quote.Services.Implementation
{
    /// <summary>
    /// Service implementation for ProcedureCoparticipation business operations.
    /// </summary>
    public class ProcedureCoparticipationService : IProcedureCoparticipationService
    {
        private readonly IProcedureCoparticipationRepository _procedureCoparticipationRepository;

        public ProcedureCoparticipationService(IProcedureCoparticipationRepository procedureCoparticipationRepository)
        {
            _procedureCoparticipationRepository = procedureCoparticipationRepository;
        }

        public IEnumerable<ProcedureCoparticipation> GetAllActiveProcedureCoparticipations()
        {
            return _procedureCoparticipationRepository.Find(cp => cp.IsActive);
        }

        public ProcedureCoparticipation? GetById(int id)
        {
            return _procedureCoparticipationRepository.GetById(id);
        }

        public void AddProcedureCoparticipation(ProcedureCoparticipation procedureCoparticipation)
        {
            _procedureCoparticipationRepository.Add(procedureCoparticipation);
        }

        public void UpdateProcedureCoparticipation(ProcedureCoparticipation procedureCoparticipation)
        {
            _procedureCoparticipationRepository.Update(procedureCoparticipation);
        }

        public void DeleteProcedureCoparticipation(int id)
        {
            _procedureCoparticipationRepository.Remove(id);
        }
    }
}