using Foundation.Base.Domain.Implementation;

namespace HealthPlanSuite.Quote.Domain.Implementation
{
    /// <summary>
    /// Plano de saúde oferecido por uma operadora
    /// </summary>
    public class Plano : Entity
    {
        public string Nome { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public int OperadoraId { get; set; }
        public int TipoPlanoId { get; set; }
        public string? Descricao { get; set; }
        public AbrangenciaGeografica AbrangenciaGeografica { get; set; }
        public TipoContratacao TipoContratacao { get; set; }
        public int IdadeMinima { get; set; } = 0;
        public int IdadeMaxima { get; set; } = 99;
        public bool Ativo { get; set; } = true;

        // Navigation properties
        public virtual Operadora Operadora { get; set; } = null!;
        public virtual TipoPlano TipoPlano { get; set; } = null!;
        public virtual ICollection<PrecoPlano> PrecosPlanos { get; set; } = new List<PrecoPlano>();
        public virtual ICollection<ItemCotacao> ItensCotacao { get; set; } = new List<ItemCotacao>();
        public virtual ICollection<CoberturaPorPlano> CoberturasPorPlano { get; set; } = new List<CoberturaPorPlano>();
    }

    public enum AbrangenciaGeografica
    {
        MUNICIPAL,
        ESTADUAL,
        REGIONAL,
        NACIONAL
    }

    public enum TipoContratacao
    {
        INDIVIDUAL,
        FAMILIAR,
        EMPRESARIAL
    }
}