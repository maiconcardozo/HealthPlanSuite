namespace HealthPlanSuite.Core.DTO
{
    public class TabelaPrecoResponseDTO
    {
        public int Id { get; set; }
        public int PlanoId { get; set; }
        public int FaixaEtariaId { get; set; }
        public decimal Mensalidade { get; set; }
        public decimal? ValorCoparticipacao { get; set; }
        public DateTime DataInicioVigencia { get; set; }
        public DateTime? DataFimVigencia { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }

        // Related data
        public string? PlanoNome { get; set; }
        public string? FaixaEtariaDescricao { get; set; }
    }
}