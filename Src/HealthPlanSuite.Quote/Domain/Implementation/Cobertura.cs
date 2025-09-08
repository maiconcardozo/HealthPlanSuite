using Foundation.Base.Domain.Implementation;

namespace HealthPlanSuite.Quote.Domain.Implementation
{
    /// <summary>
    /// Cobertura/serviço médico disponível nos planos de saúde
    /// </summary>
    public class Cobertura : Entity
    {
        public string Nome { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public CategoriaCobertura Categoria { get; set; }
        public string? Descricao { get; set; }
        public bool Obrigatoria { get; set; } = false;
        public bool Ativa { get; set; } = true;

        // Navigation properties
        public virtual ICollection<CoberturaPorPlano> CoberturasPorPlano { get; set; } = new List<CoberturaPorPlano>();
    }

    public enum CategoriaCobertura
    {
        CONSULTA,
        EXAME,
        CIRURGIA,
        INTERNACAO,
        EMERGENCIA,
        MATERNO_INFANTIL,
        ODONTOLOGIA,
        OUTROS
    }
}