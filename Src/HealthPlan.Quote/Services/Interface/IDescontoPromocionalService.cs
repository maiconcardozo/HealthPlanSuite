using HealthPlan.Quote.Domain.Implementation;

namespace HealthPlan.Quote.Services.Interface
{
    /// <summary>
    /// Service interface for DescontoPromocional business operations.
    /// Provides business logic layer for DescontoPromocional management.
    /// </summary>
    public interface IDescontoPromocionalService
    {
        IEnumerable<DescontoPromocional> GetAllActiveDescontoPromocional();
        DescontoPromocional? GetById(int id);
        void AddDescontoPromocional(DescontoPromocional descontoPromocional);
        void UpdateDescontoPromocional(DescontoPromocional descontoPromocional);
        void DeleteDescontoPromocional(int id);
    }
}