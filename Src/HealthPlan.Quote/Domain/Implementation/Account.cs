using Authentication.Login.Domain.Interface;
using Foundation.Base.Domain.Implementation;

namespace Authentication.Login.Domain.Implementation
{
    public class Account : Entity, IAccount
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
