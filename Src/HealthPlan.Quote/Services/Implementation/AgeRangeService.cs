using HealthPlan.Quote.Domain.Implementation;
using HealthPlan.Quote.Repository.Interface;
using HealthPlan.Quote.Services.Interface;

namespace HealthPlan.Quote.Services.Implementation
{
    /// <summary>
    /// Service implementation for AgeRange business operations.
    /// </summary>
    public class AgeRangeService : IAgeRangeService
    {
        private readonly IAgeRangeRepository _ageRangeRepository;

        public AgeRangeService(IAgeRangeRepository ageRangeRepository)
        {
            _ageRangeRepository = ageRangeRepository;
        }

        public IEnumerable<AgeRange> GetAllActiveAgeRanges()
        {
            return _ageRangeRepository.Find(ar => ar.IsActive);
        }

        public AgeRange? GetById(int id)
        {
            return _ageRangeRepository.GetById(id);
        }

        public void AddAgeRange(AgeRange ageRange)
        {
            _ageRangeRepository.Add(ageRange);
        }

        public void UpdateAgeRange(AgeRange ageRange)
        {
            _ageRangeRepository.Update(ageRange);
        }

        public void DeleteAgeRange(int id)
        {
            _ageRangeRepository.Remove(id);
        }
    }
}