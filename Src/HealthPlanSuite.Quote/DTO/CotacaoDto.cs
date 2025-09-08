using HealthPlanSuite.Quote.Domain.Implementation;

namespace HealthPlanSuite.Quote.DTO
{
    /// <summary>
    /// DTO para transferência de dados da Cotação
    /// </summary>
    public class CotacaoDto
    {
        public int Id { get; set; }
        public string Protocolo { get; set; } = string.Empty;
        public int BeneficiarioTitularId { get; set; }
        public string? BeneficiarioTitularNome { get; set; }
        public StatusCotacao Status { get; set; }
        public DateTime DataCotacao { get; set; }
        public DateTime DataExpiracao { get; set; }
        public string? ObservacoesCliente { get; set; }
        public string? ObservacoesInternas { get; set; }
        public decimal? ValorTotalMensal { get; set; }
        public List<ItemCotacaoDto> ItensCotacao { get; set; } = new();
        public List<BeneficiarioCotacaoDto> BeneficiariosCotacao { get; set; } = new();
    }

    /// <summary>
    /// DTO para criação de Cotação
    /// </summary>
    public class CotacaoCreateDto
    {
        public int BeneficiarioTitularId { get; set; }
        public string? ObservacoesCliente { get; set; }
        public List<BeneficiarioCotacaoCreateDto> BeneficiariosCotacao { get; set; } = new();
        public List<int> PlanosParaCotacao { get; set; } = new();
    }

    /// <summary>
    /// DTO resumido para listagem de Cotações
    /// </summary>
    public class CotacaoResumoDto
    {
        public int Id { get; set; }
        public string Protocolo { get; set; } = string.Empty;
        public string BeneficiarioTitularNome { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public StatusCotacao Status { get; set; }
        public DateTime DataCotacao { get; set; }
        public DateTime DataExpiracao { get; set; }
        public decimal? ValorTotalMensal { get; set; }
        public int QuantidadePlanos { get; set; }
        public int PlanosSelecionados { get; set; }
    }
}