namespace HealthPlanSuite.Core.DTO
{
    public class FaixaEtariaResponseDTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public int IdadeMin { get; set; }
        public int IdadeMax { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}