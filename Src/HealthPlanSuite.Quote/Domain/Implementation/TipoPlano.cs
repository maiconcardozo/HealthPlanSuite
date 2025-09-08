using Foundation.Base.Domain.Implementation;

namespace HealthPlanSuite.Quote.Domain.Implementation
{
    /// <summary>
    /// Tipo de plano de saúde conforme classificação ANS
    /// </summary>
    public class TipoPlano : Entity
    {
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public CategoriaPlano Categoria { get; set; }
        public bool Ativo { get; set; } = true;

        // Navigation properties
        public virtual ICollection<Plano> Planos { get; set; } = new List<Plano>();
    }

    public enum CategoriaPlano
    {
        AMBULATORIAL,
        HOSPITALAR,
        OBSTETRICO,
        ODONTOLOGICO
    }
}