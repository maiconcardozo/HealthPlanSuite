using HealthPlan.Quote.Repository.Interface;
using HealthPlan.Quote.UnitOfWork.Interface;
using Foundation.Base.UnitOfWork.Implementation;
using Microsoft.EntityFrameworkCore;

namespace HealthPlan.Quote.UnitOfWork.Implementation
{
    public class ApplicationUnitOfWork : BaseUnitOfWork, IApplicationUnitOfWork
    {
        public ICleanEntityRepository CleanEntityRepository { get; }

        public ApplicationUnitOfWork(
            DbContext context,
            ICleanEntityRepository cleanEntityRepository
        ) : base(context)
        {
            CleanEntityRepository = cleanEntityRepository;
        }
    }
}