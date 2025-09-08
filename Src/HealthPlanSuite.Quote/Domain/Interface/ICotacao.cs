using HealthPlanSuite.Quote.Domain.Implementation;

namespace HealthPlanSuite.Quote.Domain.Interface
{
    /// <summary>
    /// Interface para Cotação de planos de saúde
    /// </summary>
    public interface ICotacao
    {
        string Protocolo { get; set; }
        int BeneficiarioTitularId { get; set; }
        StatusCotacao Status { get; set; }
        DateTime DataCotacao { get; set; }
        DateTime DataExpiracao { get; set; }
        string? ObservacoesCliente { get; set; }
        string? ObservacoesInternas { get; set; }
        decimal? ValorTotalMensal { get; set; }
    }
}