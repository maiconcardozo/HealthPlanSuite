using HealthPlan.Domain.Entities;
using HealthPlan.Domain.Interfaces;
using HealthPlan.Application.Services;

namespace HealthPlan.Application.Services
{
    /// <summary>
    /// Service implementation for PromotionalDiscount business operations.
    /// </summary>
    public class PromotionalDiscountService : IPromotionalDiscountService
    {
        private readonly IPromotionalDiscountRepository _promotionalDiscountRepository;

        public PromotionalDiscountService(IPromotionalDiscountRepository promotionalDiscountRepository)
        {
            _promotionalDiscountRepository = promotionalDiscountRepository;
        }

        public IEnumerable<PromotionalDiscount> GetAllActivePromotionalDiscounts()
        {
            return _promotionalDiscountRepository.Find(dp => dp.IsActive);
        }

        public PromotionalDiscount? GetById(int id)
        {
            return _promotionalDiscountRepository.GetById(id);
        }

        public void AddPromotionalDiscount(PromotionalDiscount promotionalDiscount)
        {
            _promotionalDiscountRepository.Add(promotionalDiscount);
        }

        public void UpdatePromotionalDiscount(PromotionalDiscount promotionalDiscount)
        {
            _promotionalDiscountRepository.Update(promotionalDiscount);
        }

        public void DeletePromotionalDiscount(int id)
        {
            _promotionalDiscountRepository.Remove(id);
        }
    }
}