using HealthPlan.Quote.Domain.Implementation;

namespace HealthPlan.Quote.Services.Interface
{
    /// <summary>
    /// Service interface for AdhesionFee business operations.
    /// Provides business logic layer for AdhesionFee management.
    /// </summary>
    public interface IAdhesionFeeService
    {
        IEnumerable<AdhesionFee> GetAllActiveTaxaAdesao();
        AdhesionFee? GetById(int id);
        void AddTaxaAdesao(AdhesionFee taxaAdesao);
        void UpdateTaxaAdesao(AdhesionFee taxaAdesao);
        void DeleteTaxaAdesao(int id);
    }
}