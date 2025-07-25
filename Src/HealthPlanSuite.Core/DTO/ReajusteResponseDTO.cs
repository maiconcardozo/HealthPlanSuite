namespace HealthPlanSuite.Core.DTO
{
    public class ReajusteResponseDTO
    {
        public int Id { get; set; }
        public int PlanoId { get; set; }
        public decimal Percentual { get; set; }
        public DateTime DataReajuste { get; set; }
        public string TipoReajuste { get; set; } = string.Empty;
        public string? Observacao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }

        // Related data
        public string? PlanoNome { get; set; }
    }
}