using Foundation.Base.Domain.Implementation;

namespace HealthPlanSuite.Quote.Domain.Implementation
{
    /// <summary>
    /// Cobertura oferecida por um plano específico com detalhes
    /// </summary>
    public class CoberturaPorPlano : Entity
    {
        public int PlanoId { get; set; }
        public int CoberturaId { get; set; }
        public TipoCobertura TipoCobertura { get; set; } = TipoCobertura.TOTAL;
        public decimal PercentualCobertura { get; set; } = 100.00m;
        public int CarenciaEmDias { get; set; } = 0;
        public int? LimiteAnual { get; set; }
        public int? LimiteMensal { get; set; }
        public decimal ValorFranquia { get; set; } = 0.00m;
        public string? ObservacoesCobertura { get; set; }

        // Navigation properties
        public virtual Plano Plano { get; set; } = null!;
        public virtual Cobertura Cobertura { get; set; } = null!;
    }

    public enum TipoCobertura
    {
        TOTAL,
        PARCIAL,
        NAO_COBERTO
    }
}