using Foundation.Base.Repository.Interface;
using HealthPlan.Quote.Domain.HealthPlan.Implementation;

namespace HealthPlan.Quote.Repository.HealthPlan.Interface
{
    public interface IHealthEstablishmentRepository : IEntityRepository<HealthEstablishment>
    {
        IEnumerable<HealthEstablishment> GetByType(string type);
        IEnumerable<HealthEstablishment> GetByCity(string city);
        IEnumerable<HealthEstablishment> GetByState(string state);
        IEnumerable<HealthEstablishment> GetByName(string name);
    }
}