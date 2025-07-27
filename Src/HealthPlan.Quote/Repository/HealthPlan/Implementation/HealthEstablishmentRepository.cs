using Foundation.Base.Repository.Implementation;
using HealthPlan.Quote.Domain.HealthPlan.Implementation;
using HealthPlan.Quote.Repository.HealthPlan.Interface;
using Microsoft.EntityFrameworkCore;

namespace HealthPlan.Quote.Repository.HealthPlan.Implementation
{
    public class HealthEstablishmentRepository : EntityRepository<HealthEstablishment>, IHealthEstablishmentRepository
    {
        public HealthEstablishmentRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<HealthEstablishment> GetByType(string type)
        {
            return Context.Set<HealthEstablishment>()
                .Where(x => x.Type.Contains(type))
                .ToList();
        }

        public IEnumerable<HealthEstablishment> GetByCity(string city)
        {
            return Context.Set<HealthEstablishment>()
                .Where(x => x.City.Contains(city))
                .ToList();
        }

        public IEnumerable<HealthEstablishment> GetByState(string state)
        {
            return Context.Set<HealthEstablishment>()
                .Where(x => x.State.Contains(state))
                .ToList();
        }

        public IEnumerable<HealthEstablishment> GetByName(string name)
        {
            return Context.Set<HealthEstablishment>()
                .Where(x => x.Name.Contains(name))
                .ToList();
        }
    }
}