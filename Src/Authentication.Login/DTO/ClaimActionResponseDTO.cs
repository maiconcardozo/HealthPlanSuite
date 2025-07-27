namespace Authentication.Login.DTO
{
    public class ClaimActionResponseDTO
    {
        public int Id { get; set; }
        public int IdClaim { get; set; }
        public int IdAction { get; set; }
        public ClaimResponseDTO? Claim { get; set; }
        public ActionResponseDTO? Action { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}