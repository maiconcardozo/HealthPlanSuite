using HealthPlan.Quote.Domain.Implementation;
using HealthPlan.Quote.Repository.Interface;
using HealthPlan.Quote.Services.Interface;

namespace HealthPlan.Quote.Services.Implementation
{
    /// <summary>
    /// Service implementation for AdhesionFee business operations.
    /// </summary>
    public class AdhesionFeeService : IAdhesionFeeService
    {
        private readonly IAdhesionFeeRepository _taxaAdesaoRepository;

        public AdhesionFeeService(IAdhesionFeeRepository taxaAdesaoRepository)
        {
            _taxaAdesaoRepository = taxaAdesaoRepository;
        }

        public IEnumerable<AdhesionFee> GetAllActiveTaxaAdesao()
        {
            return _taxaAdesaoRepository.Find(ta => ta.IsActive);
        }

        public AdhesionFee? GetById(int id)
        {
            return _taxaAdesaoRepository.GetById(id);
        }

        public void AddTaxaAdesao(AdhesionFee taxaAdesao)
        {
            _taxaAdesaoRepository.Add(taxaAdesao);
        }

        public void UpdateTaxaAdesao(AdhesionFee taxaAdesao)
        {
            _taxaAdesaoRepository.Update(taxaAdesao);
        }

        public void DeleteTaxaAdesao(int id)
        {
            _taxaAdesaoRepository.Remove(id);
        }
    }
}