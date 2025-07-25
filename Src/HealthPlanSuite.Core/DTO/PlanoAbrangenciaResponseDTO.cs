namespace HealthPlanSuite.Core.DTO
{
    public class PlanoAbrangenciaResponseDTO
    {
        public int Id { get; set; }
        public int PlanoId { get; set; }
        public int EstabelecimentoId { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }

        // Related data
        public string? PlanoNome { get; set; }
        public string? EstabelecimentoNome { get; set; }
        public string? EstabelecimentoTipo { get; set; }
    }
}