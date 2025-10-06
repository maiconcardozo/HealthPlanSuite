using HealthPlan.Quote.Domain.Implementation;
using HealthPlan.Quote.Repository.Interface;
using HealthPlan.Quote.Services.Interface;

namespace HealthPlan.Quote.Services.Implementation
{
    /// <summary>
    /// Service implementation for PromotionalDiscount business operations.
    /// </summary>
    public class PromotionalDiscountService : IPromotionalDiscountService
    {
        private readonly IPromotionalDiscountRepository _descontoPromocionalRepository;

        public PromotionalDiscountService(IPromotionalDiscountRepository descontoPromocionalRepository)
        {
            _descontoPromocionalRepository = descontoPromocionalRepository;
        }

        public IEnumerable<PromotionalDiscount> GetAllActiveDescontoPromocional()
        {
            return _descontoPromocionalRepository.Find(dp => dp.IsActive);
        }

        public PromotionalDiscount? GetById(int id)
        {
            return _descontoPromocionalRepository.GetById(id);
        }

        public void AddDescontoPromocional(PromotionalDiscount descontoPromocional)
        {
            _descontoPromocionalRepository.Add(descontoPromocional);
        }

        public void UpdateDescontoPromocional(PromotionalDiscount descontoPromocional)
        {
            _descontoPromocionalRepository.Update(descontoPromocional);
        }

        public void DeleteDescontoPromocional(int id)
        {
            _descontoPromocionalRepository.Remove(id);
        }
    }
}