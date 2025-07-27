using HealthPlan.Quote.Domain.HealthPlan.Implementation;

namespace HealthPlan.Quote.Services.HealthPlan.Interface
{
    public interface IHealthEstablishmentService
    {
        IEnumerable<HealthEstablishment> GetAll();
        HealthEstablishment? GetById(int id);
        IEnumerable<HealthEstablishment> GetByType(string type);
        IEnumerable<HealthEstablishment> GetByCity(string city);
        IEnumerable<HealthEstablishment> GetByState(string state);
        IEnumerable<HealthEstablishment> GetByName(string name);
        HealthEstablishment Add(HealthEstablishment healthEstablishment);
        void Update(HealthEstablishment healthEstablishment);
        void Delete(int id);
    }
}