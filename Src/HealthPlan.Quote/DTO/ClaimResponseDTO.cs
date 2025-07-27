using Authentication.Login.Enum;

namespace Authentication.Login.DTO
{
    public class ClaimResponseDTO
    {
        public int Id { get; set; }
        public ClaimType Type { get; set; }
        public string Value { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}