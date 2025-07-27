namespace Authentication.Login.DTO
{
    public class AccountClaimActionResponseDTO
    {
        public int Id { get; set; }
        public int IdAccount { get; set; }
        public int IdClaimAction { get; set; }
        public AccountResponseDTO? Account { get; set; }
        public ClaimActionResponseDTO? ClaimAction { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}