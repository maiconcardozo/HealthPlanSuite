using Foundation.Base.Domain.Implementation;

namespace HealthPlanSuite.Quote.Domain.Implementation
{
    /// <summary>
    /// Endereço de um beneficiário
    /// </summary>
    public class EnderecoBeneficiario : Entity
    {
        public int BeneficiarioId { get; set; }
        public TipoEndereco TipoEndereco { get; set; } = TipoEndereco.RESIDENCIAL;
        public string CEP { get; set; } = string.Empty;
        public string Logradouro { get; set; } = string.Empty;
        public string? Numero { get; set; }
        public string? Complemento { get; set; }
        public string Bairro { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string Pais { get; set; } = "Brasil";
        public bool Principal { get; set; } = false;

        // Navigation properties
        public virtual Beneficiario Beneficiario { get; set; } = null!;
    }

    public enum TipoEndereco
    {
        RESIDENCIAL,
        COMERCIAL,
        CORRESPONDENCIA
    }
}