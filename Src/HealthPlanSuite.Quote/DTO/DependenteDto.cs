using HealthPlanSuite.Quote.Domain.Implementation;

namespace HealthPlanSuite.Quote.DTO
{
    /// <summary>
    /// DTO para transferência de dados do Dependente
    /// </summary>
    public class DependenteDto
    {
        public int Id { get; set; }
        public int BeneficiarioTitularId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public Sexo Sexo { get; set; }
        public Parentesco Parentesco { get; set; }
        public bool EstudanteAte24Anos { get; set; } = false;
        public bool PossuiDeficiencia { get; set; } = false;
    }
}