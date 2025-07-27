using Authentication.Login.Domain.Interface;

namespace Authentication.Login.Domain.Implementation
{
    public class JwtSettings : IJwtSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecretKey { get; set; }
    }
}
