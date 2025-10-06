using HealthPlan.Quote.Domain.Implementation;

namespace HealthPlan.Quote.Services.Interface
{
    /// <summary>
    /// Service interface for PromotionalDiscount business operations.
    /// Provides business logic layer for PromotionalDiscount management.
    /// </summary>
    public interface IPromotionalDiscountService
    {
        IEnumerable<PromotionalDiscount> GetAllActivePromotionalDiscounts();
        PromotionalDiscount? GetById(int id);
        void AddPromotionalDiscount(PromotionalDiscount promotionalDiscount);
        void UpdatePromotionalDiscount(PromotionalDiscount promotionalDiscount);
        void DeletePromotionalDiscount(int id);
    }
}