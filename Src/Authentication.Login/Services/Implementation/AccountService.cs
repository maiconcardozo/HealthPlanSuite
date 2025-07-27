using Authentication.Login.Domain.Implementation;
using Authentication.Login.Domain.Interface;
using Authentication.Login.Enum;
using Authentication.Login.Resource;
using Authentication.Login.Services.Interface;
using Authentication.Login.UnitOfWork.Interface;
using Foundation.Base.Util;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;

namespace Authentication.Login.Services.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly ILoginUnitOfWork _unitOfWork;

        public AccountService(ILoginUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Consultas

        public IEnumerable<Account> GetAllAccounts()
        {
            return _unitOfWork.AccountRepository.GetAll();
        }

        public Account GetAccountByUserName(string userName)
        {
            return _unitOfWork.AccountRepository.GetByUserName(userName);
        }

        public Account GetAccountByUserNameAndPassword(Account account)
        {
            var dbAccount = _unitOfWork.AccountRepository.GetByUserName(account.UserName);

            if (dbAccount == null)
                throw new InvalidOperationException(ResourceLogin.AccountNotFound);

            if (StringHelper.VerifyArgon2Hash(account.Password, dbAccount.Password))
                return dbAccount;

            throw new UnauthorizedAccessException(ResourceLogin.InvalidPassword);
        }

        public Account GetAccount(Account account)
        {
            return _unitOfWork.AccountRepository.Get(account);
        }

        public IEnumerable<Account> GetAccountsByIds(Account account)
        {
            return _unitOfWork.AccountRepository.GetByLstId(account);
        }

        public IEnumerable<Account> GetAllAccountEntities()
        {
            return _unitOfWork.AccountRepository.GetAll();
        }

        public IEnumerable<Account> GetAccounts(Expression<Func<Account, bool>> predicate)
        {
            return _unitOfWork.AccountRepository.Find(predicate);
        }

        public Account GetSingleOrDefaultAccount(Expression<Func<Account, bool>> predicate)
        {
            return _unitOfWork.AccountRepository.Find(predicate).SingleOrDefault();
        }

        public void AddAccount(Account account)
        {
            account.DtCreated = DateTime.Now;
            account.CreatedBy = "ADMINISTRATOR";

            account.Password = StringHelper.ComputeArgon2Hash(account.Password);

            _unitOfWork.ExecuteInTransaction(() =>
            {
                _unitOfWork.AccountRepository.Add(account);
            });
        }

        public void AddAccounts(IEnumerable<Account> accounts)
        {
            _unitOfWork.ExecuteInTransaction(() =>
            {
                _unitOfWork.AccountRepository.AddRange(accounts);
            });
        }

        public void UpdateAccountPassword(string userName, string newPassword)
        {
            _unitOfWork.ExecuteInTransaction(() =>
            {
                _unitOfWork.AccountRepository.UpdatePassword(UserName, newPassword);
            });
        }

        public void UpdateAccountUserName(string oldUserName, string newUserName)
        {
            _unitOfWork.ExecuteInTransaction(() =>
            {
                _unitOfWork.AccountRepository.UpdateUserName(oldUserName, newUserName);
            });
        }

        public void DeleteAccountByUserName(string userName)
        {
            _unitOfWork.ExecuteInTransaction(() =>
            {
                _unitOfWork.AccountRepository.DeleteByUserName(UserName);
            });
        }

        public void DeleteAccountsByUserNames(IEnumerable<string> userNames)
        {
            _unitOfWork.ExecuteInTransaction(() =>
            {
                _unitOfWork.AccountRepository.DeleteByUserNameList(userNames);
            });
        }

        public void DeleteAccount(Account account)
        {
            _unitOfWork.ExecuteInTransaction(() =>
            {
                _unitOfWork.AccountRepository.Remove(account);
            });
        }

        public void DeleteAccounts(IEnumerable<Account> accounts)
        {
            _unitOfWork.ExecuteInTransaction(() =>
              {
                  _unitOfWork.AccountRepository.RemoveRange(accounts);
              });
        }
        #endregion

        Token? IAccountService.GenerateToken(Account account, IJwtSettings jwtSettings)
        {
            var isAccountValid = GetAccountByUserNameAndPassword(account);

            var accountClaimActions = _unitOfWork.AccountClaimActionRepository
                .GetByIdAccount(isAccountValid.Id)
                .ToList();

            var claims = new List<System.Security.Claims.Claim>
    {
        new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, isAccountValid.UserName)
    };

            // Adiciona claims no formato "Resource:Action" (ex: "PlanoSaude:Inserir")
            claims.AddRange(accountClaimActions.Select(aca =>
                new System.Security.Claims.Claim(
                    ClaimType.Permission.ToString().ToLower(),
                    $"{aca.ClaimAction.Claim.Value}:{aca.ClaimAction.Action.Name}"
                )
            ));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            var token = new Token
            {
                AccessToken = tokenString,
                Expiration = DateTime.Now.AddHours(1),
                UserName = isAccountValid.UserName
            };

            return token;
        }

        public Account GetAccount(int accountId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Account> GetAccountsByIds(IEnumerable<int> accountIds)
        {
            throw new NotImplementedException();
        }
    }
}