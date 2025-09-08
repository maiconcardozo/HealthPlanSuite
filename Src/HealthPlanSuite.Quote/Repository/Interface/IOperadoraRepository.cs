using HealthPlanSuite.Quote.Domain.Implementation;

namespace HealthPlanSuite.Quote.Repository.Interface
{
    /// <summary>
    /// Interface do repositório de Operadoras
    /// </summary>
    public interface IOperadoraRepository
    {
        Task<IEnumerable<Operadora>> GetAllAsync();
        Task<Operadora?> GetByIdAsync(int id);
        Task<Operadora?> GetByRegistroANSAsync(string registroANS);
        Task<Operadora?> GetByCNPJAsync(string cnpj);
        Task<Operadora> CreateAsync(Operadora operadora);
        Task UpdateAsync(Operadora operadora);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsByRegistroANSAsync(string registroANS);
        Task<bool> ExistsByCNPJAsync(string cnpj);
    }
}