using HealthPlanSuite.Quote.Domain.Implementation;

namespace HealthPlanSuite.Quote.DTO
{
    /// <summary>
    /// DTO para transferência de dados do Beneficiário incluído na Cotação
    /// </summary>
    public class BeneficiarioCotacaoDto
    {
        public int Id { get; set; }
        public int CotacaoId { get; set; }
        public int? BeneficiarioId { get; set; }
        public int? DependenteId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public Sexo Sexo { get; set; }
        public ParentescoCotacao Parentesco { get; set; }
        public int FaixaEtariaId { get; set; }
        public string? FaixaEtariaNome { get; set; }
    }

    /// <summary>
    /// DTO para criação de Beneficiário na Cotação
    /// </summary>
    public class BeneficiarioCotacaoCreateDto
    {
        public int? BeneficiarioId { get; set; }
        public int? DependenteId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public Sexo Sexo { get; set; }
        public ParentescoCotacao Parentesco { get; set; }
    }
}