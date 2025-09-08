using Foundation.Base.Domain.Implementation;

namespace HealthPlanSuite.Quote.Domain.Implementation
{
    /// <summary>
    /// Dependente de um beneficiário titular
    /// </summary>
    public class Dependente : Entity
    {
        public int BeneficiarioTitularId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public Sexo Sexo { get; set; }
        public Parentesco Parentesco { get; set; }
        public bool EstudanteAte24Anos { get; set; } = false;
        public bool PossuiDeficiencia { get; set; } = false;

        // Navigation properties
        public virtual Beneficiario BeneficiarioTitular { get; set; } = null!;
        public virtual ICollection<BeneficiarioCotacao> BeneficiariosCotacao { get; set; } = new List<BeneficiarioCotacao>();
    }

    public enum Parentesco
    {
        CONJUGE,
        FILHO,
        PAI,
        MAE,
        IRMAO,
        OUTRO
    }
}