using Foundation.Base.Domain.Interface;

namespace Foundation.Base.Repository.Interface
{
    public interface IEntityRepository<T> where T : class, IEntity
    {
        IEnumerable<T> GetAll();
        T? GetById(int id);
        T Add(T entity);
        void Update(T entity);
        void Delete(int id);
        void Delete(T entity);
    }
}