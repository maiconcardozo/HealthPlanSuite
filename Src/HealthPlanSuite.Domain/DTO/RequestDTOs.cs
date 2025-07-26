namespace HealthPlanSuite.DTO
{
    // Request DTOs
    public class OperadoraRequestDTO
    {
        public string Nome { get; set; } = string.Empty;
        public string CNPJ { get; set; } = string.Empty;
        public string? Site { get; set; }
        public string? Telefone { get; set; }
    }

    public class TipoPlanoRequestDTO
    {
        public string Descricao { get; set; } = string.Empty;
        public string? RegulacaoANS { get; set; }
    }

    public class FaixaEtariaRequestDTO
    {
        public string Descricao { get; set; } = string.Empty;
        public int IdadeMin { get; set; }
        public int IdadeMax { get; set; }
    }

    public class EstabelecimentoSaudeRequestDTO
    {
        public string Nome { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
    }

    public class PlanoRequestDTO
    {
        public int OperadoraId { get; set; }
        public int TipoPlanoId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cobertura { get; set; } = string.Empty;
        public bool PossuiCoparticipacao { get; set; }
    }

    public class TabelaPrecoRequestDTO
    {
        public int PlanoId { get; set; }
        public int FaixaEtariaId { get; set; }
        public decimal Mensalidade { get; set; }
        public decimal? ValorCoparticipacao { get; set; }
        public DateTime DataInicioVigencia { get; set; }
        public DateTime? DataFimVigencia { get; set; }
    }

    public class ReajusteRequestDTO
    {
        public int PlanoId { get; set; }
        public decimal Percentual { get; set; }
        public DateTime DataReajuste { get; set; }
        public string TipoReajuste { get; set; } = string.Empty;
        public string? Observacao { get; set; }
    }

    public class PlanoAbrangenciaRequestDTO
    {
        public int PlanoId { get; set; }
        public int EstabelecimentoId { get; set; }
    }
}