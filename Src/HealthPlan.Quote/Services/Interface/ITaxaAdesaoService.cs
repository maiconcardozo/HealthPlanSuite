using HealthPlan.Quote.Domain.Implementation;

namespace HealthPlan.Quote.Services.Interface
{
    /// <summary>
    /// Service interface for TaxaAdesao business operations.
    /// Provides business logic layer for TaxaAdesao management.
    /// </summary>
    public interface ITaxaAdesaoService
    {
        IEnumerable<TaxaAdesao> GetAllActiveTaxaAdesao();
        TaxaAdesao? GetById(int id);
        void AddTaxaAdesao(TaxaAdesao taxaAdesao);
        void UpdateTaxaAdesao(TaxaAdesao taxaAdesao);
        void DeleteTaxaAdesao(int id);
    }
}