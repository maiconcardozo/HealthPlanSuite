using HealthPlanSuite.Quote.Domain.Implementation;

namespace HealthPlanSuite.Quote.DTO
{
    /// <summary>
    /// DTO para transferência de dados do Endereço do Beneficiário
    /// </summary>
    public class EnderecoBeneficiarioDto
    {
        public int Id { get; set; }
        public int BeneficiarioId { get; set; }
        public TipoEndereco TipoEndereco { get; set; }
        public string CEP { get; set; } = string.Empty;
        public string Logradouro { get; set; } = string.Empty;
        public string? Numero { get; set; }
        public string? Complemento { get; set; }
        public string Bairro { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string Pais { get; set; } = "Brasil";
        public bool Principal { get; set; } = false;
    }
}