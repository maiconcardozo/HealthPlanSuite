using Foundation.Base.Domain.Implementation;

namespace HealthPlanSuite.Quote.Domain.Implementation
{
    /// <summary>
    /// Operadora de plano de saúde registrada na ANS
    /// </summary>
    public class Operadora : Entity
    {
        public string Nome { get; set; } = string.Empty;
        public string RegistroANS { get; set; } = string.Empty;
        public string CNPJ { get; set; } = string.Empty;
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public string? Site { get; set; }
        public bool Ativa { get; set; } = true;

        // Navigation properties
        public virtual ICollection<Plano> Planos { get; set; } = new List<Plano>();
    }
}