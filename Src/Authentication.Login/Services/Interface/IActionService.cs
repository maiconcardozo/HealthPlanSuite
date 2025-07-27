using Authentication.Login.Domain.Implementation;

namespace Authentication.Login.Services.Interface
{
    public interface IActionService
    {
        IEnumerable<Action> GetAll();
        Action? GetById(int id);
        Action? GetByName(string name);
        void AddAction(Action action);
        void UpdateAction(Action action);
        void DeleteAction(int id);
    }
}