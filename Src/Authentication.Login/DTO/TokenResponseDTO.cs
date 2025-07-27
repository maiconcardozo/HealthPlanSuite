namespace Authentication.Login.DTO
{
    public class TokenResponseDTO
    {
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; }
        public string UserName { get; set; }
    }
}
