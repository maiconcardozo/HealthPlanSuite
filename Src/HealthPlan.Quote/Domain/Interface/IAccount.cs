using Foundation.Base.Domain.Interface;

namespace Authentication.Login.Domain.Interface
{
    public interface IAccount : IEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
