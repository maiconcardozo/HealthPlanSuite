using Foundation.Base.Domain.Implementation;

namespace HealthPlanSuite.Quote.Domain.Implementation
{
    /// <summary>
    /// Beneficiário incluído na cotação (titular ou dependente)
    /// </summary>
    public class BeneficiarioCotacao : Entity
    {
        public int CotacaoId { get; set; }
        public int? BeneficiarioId { get; set; }
        public int? DependenteId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public Sexo Sexo { get; set; }
        public ParentescoCotacao Parentesco { get; set; }
        public int FaixaEtariaId { get; set; }

        // Navigation properties
        public virtual Cotacao Cotacao { get; set; } = null!;
        public virtual Beneficiario? Beneficiario { get; set; }
        public virtual Dependente? Dependente { get; set; }
        public virtual FaixaEtaria FaixaEtaria { get; set; } = null!;
    }

    public enum ParentescoCotacao
    {
        TITULAR,
        CONJUGE,
        FILHO,
        PAI,
        MAE,
        IRMAO,
        OUTRO
    }
}