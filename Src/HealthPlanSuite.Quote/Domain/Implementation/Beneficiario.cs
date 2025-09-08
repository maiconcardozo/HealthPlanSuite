using Foundation.Base.Domain.Implementation;

namespace HealthPlanSuite.Quote.Domain.Implementation
{
    /// <summary>
    /// Beneficiário/segurado titular de plano de saúde
    /// </summary>
    public class Beneficiario : Entity
    {
        public string Nome { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public string? RG { get; set; }
        public DateTime DataNascimento { get; set; }
        public Sexo Sexo { get; set; }
        public EstadoCivil EstadoCivil { get; set; }
        public string? Telefone { get; set; }
        public string? Celular { get; set; }
        public string? Email { get; set; }
        public string? Profissao { get; set; }
        public decimal? RendaFamiliar { get; set; }
        public bool PossuiPlanoSaude { get; set; } = false;
        public string? PlanoAtual { get; set; }

        // Navigation properties
        public virtual ICollection<EnderecoBeneficiario> Enderecos { get; set; } = new List<EnderecoBeneficiario>();
        public virtual ICollection<Dependente> Dependentes { get; set; } = new List<Dependente>();
        public virtual ICollection<Cotacao> Cotacoes { get; set; } = new List<Cotacao>();
        public virtual ICollection<BeneficiarioCotacao> BeneficiariosCotacao { get; set; } = new List<BeneficiarioCotacao>();
    }

    public enum Sexo
    {
        M,
        F
    }

    public enum EstadoCivil
    {
        SOLTEIRO,
        CASADO,
        DIVORCIADO,
        VIUVO,
        UNIAO_ESTAVEL
    }
}