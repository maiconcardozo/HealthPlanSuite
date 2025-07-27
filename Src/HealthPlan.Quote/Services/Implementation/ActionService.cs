using Authentication.Login.Domain.Implementation;
using Authentication.Login.Repository.Interface;
using Authentication.Login.Services.Interface;
using System.Collections.Generic;
using ActionEntity = Authentication.Login.Domain.Implementation.Action;

namespace Authentication.Login.Services.Implementation
{
    public class ActionService : IActionService
    {
        private readonly IActionRepository _actionRepository;

        public ActionService(IActionRepository actionRepository)
        {
            _actionRepository = actionRepository;
        }

        public IEnumerable<ActionEntity> GetAll() => _actionRepository.GetAll();

        public ActionEntity? GetById(int id) => _actionRepository.GetById(id);

        public ActionEntity? GetByName(string name) => _actionRepository.GetByName(name);

        public void AddAction(ActionEntity action) => _actionRepository.Add(action);

        public void UpdateAction(ActionEntity action) => _actionRepository.Update(action);

        public void DeleteAction(int id)
        {
            var action = _actionRepository.GetById(id);
            if (action != null)
                _actionRepository.Remove(action);
        }
    }
}