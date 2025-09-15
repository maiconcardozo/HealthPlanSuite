using HealthPlan.Quote.Domain.Implementation;
using HealthPlan.Quote.Repository.Interface;
using HealthPlan.Quote.Services.Interface;

namespace HealthPlan.Quote.Services.Implementation
{
    /// <summary>
    /// Service implementation for TaxaAdesao business operations.
    /// </summary>
    public class TaxaAdesaoService : ITaxaAdesaoService
    {
        private readonly ITaxaAdesaoRepository _taxaAdesaoRepository;

        public TaxaAdesaoService(ITaxaAdesaoRepository taxaAdesaoRepository)
        {
            _taxaAdesaoRepository = taxaAdesaoRepository;
        }

        public IEnumerable<TaxaAdesao> GetAllActiveTaxaAdesao()
        {
            return _taxaAdesaoRepository.Find(ta => ta.IsActive);
        }

        public TaxaAdesao? GetById(int id)
        {
            return _taxaAdesaoRepository.GetById(id);
        }

        public void AddTaxaAdesao(TaxaAdesao taxaAdesao)
        {
            _taxaAdesaoRepository.Add(taxaAdesao);
        }

        public void UpdateTaxaAdesao(TaxaAdesao taxaAdesao)
        {
            _taxaAdesaoRepository.Update(taxaAdesao);
        }

        public void DeleteTaxaAdesao(int id)
        {
            _taxaAdesaoRepository.Remove(id);
        }
    }
}