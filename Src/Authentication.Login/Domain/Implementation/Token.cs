namespace Authentication.Login.Domain.Implementation
{
    public class Token
    {
        public required string AccessToken { get; set; }
        public DateTime Expiration { get; set; }
        public string UserName { get; set; }
    }
}
