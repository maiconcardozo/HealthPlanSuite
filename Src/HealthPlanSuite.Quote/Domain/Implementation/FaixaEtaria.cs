using Foundation.Base.Domain.Implementation;

namespace HealthPlanSuite.Quote.Domain.Implementation
{
    /// <summary>
    /// Faixa etária para cálculo de preços dos planos
    /// </summary>
    public class FaixaEtaria : Entity
    {
        public string Nome { get; set; } = string.Empty;
        public int IdadeMinima { get; set; }
        public int IdadeMaxima { get; set; }
        public bool Ativa { get; set; } = true;

        // Navigation properties
        public virtual ICollection<PrecoPlano> PrecosPlanos { get; set; } = new List<PrecoPlano>();
        public virtual ICollection<BeneficiarioCotacao> BeneficiariosCotacao { get; set; } = new List<BeneficiarioCotacao>();
    }
}