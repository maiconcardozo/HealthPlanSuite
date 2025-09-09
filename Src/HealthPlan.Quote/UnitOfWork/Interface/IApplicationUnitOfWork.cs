using HealthPlan.Quote.Repository.Interface;
using Foundation.Base.UnitOfWork.Interface;

namespace HealthPlan.Quote.UnitOfWork.Interface
{
    public interface IApplicationUnitOfWork : IBaseUnitOfWork
    {
        ICleanEntityRepository CleanEntityRepository { get; }
    }
}