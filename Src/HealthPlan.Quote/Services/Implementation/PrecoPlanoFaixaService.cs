using HealthPlan.Quote.Domain.Implementation;
using HealthPlan.Quote.Repository.Interface;
using HealthPlan.Quote.Services.Interface;

namespace HealthPlan.Quote.Services.Implementation
{
    /// <summary>
    /// Service implementation for PrecoPlanoFaixa business operations.
    /// </summary>
    public class PrecoPlanoFaixaService : IPrecoPlanoFaixaService
    {
        private readonly IPrecoPlanoFaixaRepository _precoPlanoFaixaRepository;

        public PrecoPlanoFaixaService(IPrecoPlanoFaixaRepository precoPlanoFaixaRepository)
        {
            _precoPlanoFaixaRepository = precoPlanoFaixaRepository;
        }

        public IEnumerable<PrecoPlanoFaixa> GetAllActivePrecoPlanoFaixa()
        {
            return _precoPlanoFaixaRepository.Find(ppf => ppf.IsActive);
        }

        public PrecoPlanoFaixa? GetById(int id)
        {
            return _precoPlanoFaixaRepository.GetById(id);
        }

        public void AddPrecoPlanoFaixa(PrecoPlanoFaixa precoPlanoFaixa)
        {
            _precoPlanoFaixaRepository.Add(precoPlanoFaixa);
        }

        public void UpdatePrecoPlanoFaixa(PrecoPlanoFaixa precoPlanoFaixa)
        {
            _precoPlanoFaixaRepository.Update(precoPlanoFaixa);
        }

        public void DeletePrecoPlanoFaixa(int id)
        {
            _precoPlanoFaixaRepository.Remove(id);
        }
    }
}