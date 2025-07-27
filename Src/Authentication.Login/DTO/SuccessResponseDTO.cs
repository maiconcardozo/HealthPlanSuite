namespace Authentication.Login.DTO
{
    public class SuccessResponseDTO
    {
        public int Status { get; set; }
        public required string Message { get; set; }
        public required object Data { get; set; }
    }
}
