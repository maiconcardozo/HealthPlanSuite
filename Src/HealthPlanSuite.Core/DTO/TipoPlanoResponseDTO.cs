namespace HealthPlanSuite.Core.DTO
{
    public class TipoPlanoResponseDTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public string RegulacaoANS { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}