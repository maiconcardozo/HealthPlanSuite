using HealthPlan.Quote.Domain.HealthPlan.Implementation;
using HealthPlan.Quote.Services.HealthPlan.Interface;
using HealthPlan.Quote.UnitOfWork.Interface;

namespace HealthPlan.Quote.Services.HealthPlan.Implementation
{
    public class HealthEstablishmentService : IHealthEstablishmentService
    {
        private readonly IHealthPlanUnitOfWork _unitOfWork;

        public HealthEstablishmentService(IHealthPlanUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<HealthEstablishment> GetAll()
        {
            return _unitOfWork.HealthEstablishmentRepository.GetAll();
        }

        public HealthEstablishment? GetById(int id)
        {
            return _unitOfWork.HealthEstablishmentRepository.GetById(id);
        }

        public IEnumerable<HealthEstablishment> GetByName(string name)
        {
            return _unitOfWork.HealthEstablishmentRepository.GetByName(name);
        }

        public IEnumerable<HealthEstablishment> GetByType(string type)
        {
            return _unitOfWork.HealthEstablishmentRepository.GetByType(type);
        }

        public IEnumerable<HealthEstablishment> GetByCity(string city)
        {
            return _unitOfWork.HealthEstablishmentRepository.GetByCity(city);
        }

        public IEnumerable<HealthEstablishment> GetByState(string state)
        {
            return _unitOfWork.HealthEstablishmentRepository.GetByState(state);
        }

        public HealthEstablishment Add(HealthEstablishment healthEstablishment)
        {
            HealthEstablishment result = null;
            _unitOfWork.ExecuteInTransaction(() =>
            {
                result = _unitOfWork.HealthEstablishmentRepository.Add(healthEstablishment);
            });
            return result;
        }

        public void Update(HealthEstablishment healthEstablishment)
        {
            _unitOfWork.ExecuteInTransaction(() =>
            {
                _unitOfWork.HealthEstablishmentRepository.Update(healthEstablishment);
            });
        }

        public void Delete(int id)
        {
            _unitOfWork.ExecuteInTransaction(() =>
            {
                _unitOfWork.HealthEstablishmentRepository.Delete(id);
            });
        }
    }
}