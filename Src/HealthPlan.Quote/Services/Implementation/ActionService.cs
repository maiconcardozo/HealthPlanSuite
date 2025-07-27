using Authentication.Login.Domain.Implementation;
using Authentication.Login.Repository.Interface;
using Authentication.Login.Services.Interface;
using System.Collections.Generic;

namespace Authentication.Login.Services.Implementation
{
    public class ActionService : IActionService
    {
        private readonly IActionRepository _actionRepository;

        public ActionService(IActionRepository actionRepository)
        {
            _actionRepository = actionRepository;
        }

        public IEnumerable<Action> GetAll() => _actionRepository.GetAll();

        public Action? GetById(int id) => _actionRepository.GetById(id);

        public Action? GetByName(string name) => _actionRepository.GetByName(name);

        public void AddAction(Action action) => _actionRepository.Add(action);

        public void UpdateAction(Action action) => _actionRepository.Update(action);

        public void DeleteAction(int id)
        {
            var action = _actionRepository.GetById(id);
            if (action != null)
                _actionRepository.Remove(action);
        }
    }
}