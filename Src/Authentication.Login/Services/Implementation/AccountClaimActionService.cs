using Authentication.Login.Domain.Implementation;
using Authentication.Login.Repository.Interface;
using Authentication.Login.Services.Interface;
using System.Collections.Generic;

namespace Authentication.Login.Services.Implementation
{
    public class AccountClaimActionService : IAccountClaimActionService
    {
        private readonly IAccountClaimActionRepository _repo;

        public AccountClaimActionService(IAccountClaimActionRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<AccountClaimAction> GetByIdAccount(int idAccount) =>
            _repo.GetByIdAccount(idAccount);

        public IEnumerable<AccountClaimAction> GetByIdClaimAction(int idClaimAction) =>
            _repo.GetByIdClaimAction(idClaimAction);

        public AccountClaimAction? GetByAccountAndClaimAction(int idAccount, int idClaimAction) =>
            _repo.GetByAccountAndClaimAction(idAccount, idClaimAction);

        public void AddAccountClaimAction(AccountClaimAction accountClaimAction) =>
            _repo.Add(accountClaimAction);

        public void UpdateAccountClaimAction(AccountClaimAction accountClaimAction) =>
            _repo.Update(accountClaimAction);

        public void DeleteAccountClaimAction(int id)
        {
            var entity = _repo.GetById(id);
            if (entity != null)
                _repo.Remove(entity);
        }
    }
}