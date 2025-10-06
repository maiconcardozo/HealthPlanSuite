using HealthPlan.Quote.Domain.Implementation;

namespace HealthPlan.Quote.Services.Interface
{
    /// <summary>
    /// Service interface for PromotionalDiscount business operations.
    /// Provides business logic layer for PromotionalDiscount management.
    /// </summary>
    public interface IPromotionalDiscountService
    {
        IEnumerable<PromotionalDiscount> GetAllActiveDescontoPromocional();
        PromotionalDiscount? GetById(int id);
        void AddDescontoPromocional(PromotionalDiscount descontoPromocional);
        void UpdateDescontoPromocional(PromotionalDiscount descontoPromocional);
        void DeleteDescontoPromocional(int id);
    }
}