using HealthPlan.Quote.Domain.Implementation;
using HealthPlan.Quote.Repository.Interface;
using HealthPlan.Quote.Services.Interface;

namespace HealthPlan.Quote.Services.Implementation
{
    /// <summary>
    /// Service implementation for DescontoPromocional business operations.
    /// </summary>
    public class DescontoPromocionalService : IDescontoPromocionalService
    {
        private readonly IDescontoPromocionalRepository _descontoPromocionalRepository;

        public DescontoPromocionalService(IDescontoPromocionalRepository descontoPromocionalRepository)
        {
            _descontoPromocionalRepository = descontoPromocionalRepository;
        }

        public IEnumerable<DescontoPromocional> GetAllActiveDescontoPromocional()
        {
            return _descontoPromocionalRepository.Find(dp => dp.IsActive);
        }

        public DescontoPromocional? GetById(int id)
        {
            return _descontoPromocionalRepository.GetById(id);
        }

        public void AddDescontoPromocional(DescontoPromocional descontoPromocional)
        {
            _descontoPromocionalRepository.Add(descontoPromocional);
        }

        public void UpdateDescontoPromocional(DescontoPromocional descontoPromocional)
        {
            _descontoPromocionalRepository.Update(descontoPromocional);
        }

        public void DeleteDescontoPromocional(int id)
        {
            _descontoPromocionalRepository.Remove(id);
        }
    }
}