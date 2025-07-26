namespace HealthPlanSuite.DTO
{
    // Response DTOs
    public class OperadoraResponseDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string CNPJ { get; set; } = string.Empty;
        public string? Site { get; set; }
        public string? Telefone { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class TipoPlanoResponseDTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public string? RegulacaoANS { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class FaixaEtariaResponseDTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public int IdadeMin { get; set; }
        public int IdadeMax { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class EstabelecimentoSaudeResponseDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class PlanoResponseDTO
    {
        public int Id { get; set; }
        public int OperadoraId { get; set; }
        public int TipoPlanoId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cobertura { get; set; } = string.Empty;
        public bool PossuiCoparticipacao { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties for detailed responses
        public OperadoraResponseDTO? Operadora { get; set; }
        public TipoPlanoResponseDTO? TipoPlano { get; set; }
    }

    public class TabelaPrecoResponseDTO
    {
        public int Id { get; set; }
        public int PlanoId { get; set; }
        public int FaixaEtariaId { get; set; }
        public decimal Mensalidade { get; set; }
        public decimal? ValorCoparticipacao { get; set; }
        public DateTime DataInicioVigencia { get; set; }
        public DateTime? DataFimVigencia { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public PlanoResponseDTO? Plano { get; set; }
        public FaixaEtariaResponseDTO? FaixaEtaria { get; set; }
    }

    public class ReajusteResponseDTO
    {
        public int Id { get; set; }
        public int PlanoId { get; set; }
        public decimal Percentual { get; set; }
        public DateTime DataReajuste { get; set; }
        public string TipoReajuste { get; set; } = string.Empty;
        public string? Observacao { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public PlanoResponseDTO? Plano { get; set; }
    }

    public class PlanoAbrangenciaResponseDTO
    {
        public int Id { get; set; }
        public int PlanoId { get; set; }
        public int EstabelecimentoId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Navigation properties
        public PlanoResponseDTO? Plano { get; set; }
        public EstabelecimentoSaudeResponseDTO? EstabelecimentoSaude { get; set; }
    }
}