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
        private readonly IAdhesionFeeRepository _adhesionFeeRepository;

        public AdhesionFeeService(IAdhesionFeeRepository adhesionFeeRepository)
        {
            _adhesionFeeRepository = adhesionFeeRepository;
        }

        public IEnumerable<AdhesionFee> GetAllActiveAdhesionFees()
        {
            return _adhesionFeeRepository.Find(ta => ta.IsActive);
        }

        public AdhesionFee? GetById(int id)
        {
            return _adhesionFeeRepository.GetById(id);
        }

        public void AddAdhesionFee(AdhesionFee adhesionFee)
        {
            _adhesionFeeRepository.Add(adhesionFee);
        }

        public void UpdateAdhesionFee(AdhesionFee adhesionFee)
        {
            _adhesionFeeRepository.Update(adhesionFee);
        }

        public void DeleteAdhesionFee(int id)
        {
            _adhesionFeeRepository.Remove(id);
        }
    }
}