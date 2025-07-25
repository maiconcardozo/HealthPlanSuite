namespace HealthPlanSuite.Core.DTO
{
    public class PlanoResponseDTO
    {
        public int Id { get; set; }
        public int OperadoraId { get; set; }
        public int TipoPlanoId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Cobertura { get; set; }
        public bool PossuiCoparticipacao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }

        // Related data
        public string? OperadoraNome { get; set; }
        public string? TipoPlanoDescricao { get; set; }
    }
}