using HealthPlan.Quote.Domain.Implementation;
using HealthPlan.Quote.Repository.Interface;
using HealthPlan.Quote.Services.Interface;

namespace HealthPlan.Quote.Services.Implementation
{
    /// <summary>
    /// Service implementation for CoparticipacaoProcedimento business operations.
    /// </summary>
    public class CoparticipacaoProcedimentoService : ICoparticipacaoProcedimentoService
    {
        private readonly ICoparticipacaoProcedimentoRepository _coparticipacaoProcedimentoRepository;

        public CoparticipacaoProcedimentoService(ICoparticipacaoProcedimentoRepository coparticipacaoProcedimentoRepository)
        {
            _coparticipacaoProcedimentoRepository = coparticipacaoProcedimentoRepository;
        }

        public IEnumerable<CoparticipacaoProcedimento> GetAllActiveCoparticipacaoProcedimento()
        {
            return _coparticipacaoProcedimentoRepository.Find(cp => cp.IsActive);
        }

        public CoparticipacaoProcedimento? GetById(int id)
        {
            return _coparticipacaoProcedimentoRepository.GetById(id);
        }

        public void AddCoparticipacaoProcedimento(CoparticipacaoProcedimento coparticipacaoProcedimento)
        {
            _coparticipacaoProcedimentoRepository.Add(coparticipacaoProcedimento);
        }

        public void UpdateCoparticipacaoProcedimento(CoparticipacaoProcedimento coparticipacaoProcedimento)
        {
            _coparticipacaoProcedimentoRepository.Update(coparticipacaoProcedimento);
        }

        public void DeleteCoparticipacaoProcedimento(int id)
        {
            _coparticipacaoProcedimentoRepository.Remove(id);
        }
    }
}