using HealthPlanSuite.Quote.Domain.Implementation;

namespace HealthPlanSuite.Quote.DTO
{
    /// <summary>
    /// DTO para transferência de dados do Beneficiário
    /// </summary>
    public class BeneficiarioDto
    {
        public int Id { get; set; }
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
        public List<EnderecoBeneficiarioDto> Enderecos { get; set; } = new();
        public List<DependenteDto> Dependentes { get; set; } = new();
    }

    /// <summary>
    /// DTO para criação/atualização de Beneficiário
    /// </summary>
    public class BeneficiarioCreateDto
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
    }
}