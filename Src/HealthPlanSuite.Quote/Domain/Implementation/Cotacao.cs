using Foundation.Base.Domain.Implementation;

namespace HealthPlanSuite.Quote.Domain.Implementation
{
    /// <summary>
    /// Cotação de planos de saúde solicitada por um beneficiário
    /// </summary>
    public class Cotacao : Entity
    {
        public string Protocolo { get; set; } = string.Empty;
        public int BeneficiarioTitularId { get; set; }
        public StatusCotacao Status { get; set; } = StatusCotacao.PENDENTE;
        public DateTime DataCotacao { get; set; } = DateTime.UtcNow;
        public DateTime DataExpiracao { get; set; }
        public string? ObservacoesCliente { get; set; }
        public string? ObservacoesInternas { get; set; }
        public decimal? ValorTotalMensal { get; set; }

        // Navigation properties
        public virtual Beneficiario BeneficiarioTitular { get; set; } = null!;
        public virtual ICollection<ItemCotacao> ItensCotacao { get; set; } = new List<ItemCotacao>();
        public virtual ICollection<BeneficiarioCotacao> BeneficiariosCotacao { get; set; } = new List<BeneficiarioCotacao>();
    }

    public enum StatusCotacao
    {
        PENDENTE,
        EM_ANALISE,
        APROVADA,
        REJEITADA,
        EXPIRADA
    }
}