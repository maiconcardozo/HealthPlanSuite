using System.Linq.Expressions;
using Authentication.Login.Domain.Implementation;
using Authentication.Login.Domain.Interface;

namespace Authentication.Login.Services.Interface
{
    public interface IAccountService
    {
        IEnumerable<Account> GetAllAccounts();
        Account GetAccountByUserName(string userName);
        Account GetAccountByUserNameAndPassword(Account account);
        void UpdateAccountPassword(string userName, string newPassword);
        void UpdateAccountUserName(string oldUserName, string newUserName);
        void DeleteAccountByUserName(string userName);
        void DeleteAccountsByUserNames(IEnumerable<string> userNames);
        Account GetAccount(int accountId);
        IEnumerable<Account> GetAccountsByIds(IEnumerable<int> accountIds);
        IEnumerable<Account> GetAllAccountEntities();
        IEnumerable<Account> GetAccounts(Expression<Func<Account, bool>> predicate);
        Account GetSingleOrDefaultAccount(Expression<Func<Account, bool>> predicate);
        void AddAccount(Account account);
        void AddAccounts(IEnumerable<Account> accounts);
        void DeleteAccount(Account account);
        void DeleteAccounts(IEnumerable<Account> accounts);
        Token? GenerateToken(Account account, IJwtSettings jwtSettings);
    }
}