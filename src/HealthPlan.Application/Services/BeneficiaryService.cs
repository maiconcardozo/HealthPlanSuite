using HealthPlan.Domain.Entities;
using HealthPlan.Domain.Interfaces;
using HealthPlan.Application.Services;

namespace HealthPlan.Application.Services
{
    /// <summary>
    /// Service implementation for Beneficiary business operations.
    /// </summary>
    public class BeneficiaryService : IBeneficiaryService
    {
        private readonly IBeneficiaryRepository _beneficiaryRepository;

        public BeneficiaryService(IBeneficiaryRepository beneficiaryRepository)
        {
            _beneficiaryRepository = beneficiaryRepository;
        }

        public IEnumerable<Beneficiary> GetAllActiveBeneficiaries()
        {
            return _beneficiaryRepository.Find(b => b.IsActive);
        }

        public Beneficiary? GetById(int id)
        {
            return _beneficiaryRepository.GetById(id);
        }

        public void AddBeneficiary(Beneficiary beneficiary)
        {
            _beneficiaryRepository.Add(beneficiary);
        }

        public void UpdateBeneficiary(Beneficiary beneficiary)
        {
            _beneficiaryRepository.Update(beneficiary);
        }

        public void DeleteBeneficiary(int id)
        {
            _beneficiaryRepository.Remove(id);
        }
    }
}