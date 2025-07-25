namespace HealthPlanSuite.Core.DTO
{
    public class OperadoraResponseDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string CNPJ { get; set; } = string.Empty;
        public string? Site { get; set; }
        public string? Telefone { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}