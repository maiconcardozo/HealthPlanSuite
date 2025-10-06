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
        private readonly IProcedureCoparticipationRepository _coparticipacaoProcedimentoRepository;

        public ProcedureCoparticipationService(IProcedureCoparticipationRepository coparticipacaoProcedimentoRepository)
        {
            _coparticipacaoProcedimentoRepository = coparticipacaoProcedimentoRepository;
        }

        public IEnumerable<ProcedureCoparticipation> GetAllActiveCoparticipacaoProcedimento()
        {
            return _coparticipacaoProcedimentoRepository.Find(cp => cp.IsActive);
        }

        public ProcedureCoparticipation? GetById(int id)
        {
            return _coparticipacaoProcedimentoRepository.GetById(id);
        }

        public void AddCoparticipacaoProcedimento(ProcedureCoparticipation coparticipacaoProcedimento)
        {
            _coparticipacaoProcedimentoRepository.Add(coparticipacaoProcedimento);
        }

        public void UpdateCoparticipacaoProcedimento(ProcedureCoparticipation coparticipacaoProcedimento)
        {
            _coparticipacaoProcedimentoRepository.Update(coparticipacaoProcedimento);
        }

        public void DeleteCoparticipacaoProcedimento(int id)
        {
            _coparticipacaoProcedimentoRepository.Remove(id);
        }
    }
}