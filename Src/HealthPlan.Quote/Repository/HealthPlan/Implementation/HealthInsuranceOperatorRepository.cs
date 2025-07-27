using Foundation.Base.Repository.Implementation;
using HealthPlan.Quote.Domain.HealthPlan.Implementation;
using HealthPlan.Quote.Repository.HealthPlan.Interface;
using Microsoft.EntityFrameworkCore;

namespace HealthPlan.Quote.Repository.HealthPlan.Implementation
{
    public class HealthInsuranceOperatorRepository : EntityRepository<HealthInsuranceOperator>, IHealthInsuranceOperatorRepository
    {
        public HealthInsuranceOperatorRepository(DbContext context) : base(context)
        {
        }

        public HealthInsuranceOperator? GetByCNPJ(string cnpj)
        {
            return Context.Set<HealthInsuranceOperator>()
                .FirstOrDefault(x => x.CNPJ == cnpj);
        }

        public IEnumerable<HealthInsuranceOperator> GetByName(string name)
        {
            return Context.Set<HealthInsuranceOperator>()
                .Where(x => x.Name.Contains(name))
                .ToList();
        }
    }
}