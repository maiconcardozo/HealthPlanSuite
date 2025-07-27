using Authentication.Login.Domain.Implementation;
using Authentication.Login.Domain.Interface;
using Authentication.Login.Repository.Interface;
using Foundation.Base.Repository.Implementation;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Login.Repository.Implementation
{
    public class AccountRepository : EntityRepository<Account>, IAccountRepository
    {
        private readonly DbContext _context;

        public AccountRepository(DbContext context) : base(context)
        {
            _context = context;
        }

        public Account? GetByUserName(string UserName)
        {
            return Context.Set<Account>().FirstOrDefault(a => a.UserName == UserName);
        }

        public IEnumerable<Account> GetByUserNameAndPasswordList(IEnumerable<string> UserNames)
        {
            return Context.Set<Account>().Where(a => UserNames.Contains(a.UserName)).ToList();
        }

        public IEnumerable<Account> GetByUserNameList(IEnumerable<string> UserNames)
        {
            return Context.Set<Account>().Where(a => UserNames.Contains(a.UserName)).ToList();
        }

        public void UpdatePassword(string UserName, string newPassword)
        {
            var account = GetByUserName(UserName);
            if (account != null)
            {
                account.Password = newPassword;
                Context.Set<Account>().Update(account);
            }
        }

        public void UpdateUserName(string oldUserName, string newUserName)
        {
            var account = GetByUserName(oldUserName);
            if (account != null)
            {
                account.UserName = newUserName;
                Context.Set<Account>().Update(account);
            }
        }

        public void DeleteByUserName(string UserName)
        {
            var account = GetByUserName(UserName);
            if (account != null)
            {
                Context.Set<Account>().Remove(account);
            }
        }

        public void DeleteByUserNameList(IEnumerable<string> UserNames)
        {
            var accounts = GetByUserNameList(UserNames);
            if (accounts != null && accounts.Any())
            {
                Context.Set<Account>().RemoveRange(accounts);
            }
        }

        public void DeleteByUserNameAndPassword(Account account)
        {
            var accountDeleted = GetByUserName(account.UserName);

            if (accountDeleted != null)
            {
                Context.Set<Account>().Remove(accountDeleted);
            }
        }

        public void DeleteByUserNameAndPasswordList(IEnumerable<(string UserName, string password)> UserNamePasswordPairs)
        {
            var accounts = GetByUserNameAndPasswordList(UserNamePasswordPairs.Select(lp => lp.UserName).ToList());

            if (accounts != null && accounts.Any())
            {
                Context.Set<Account>().RemoveRange(accounts);
            }
        }

        public void DeleteByUserNameAndPasswordList(IEnumerable<(string UserName, string password)> UserNamePasswordPairs, bool isDelete)
        {
            if (isDelete)
            {
                DeleteByUserNameAndPasswordList(UserNamePasswordPairs);
            }
        }

        public void DeleteByUserNameList(IEnumerable<string> UserNames, bool isDelete)
        {
            if (isDelete)
            {
                DeleteByUserNameList(UserNames);
            }
        }
    }
}
