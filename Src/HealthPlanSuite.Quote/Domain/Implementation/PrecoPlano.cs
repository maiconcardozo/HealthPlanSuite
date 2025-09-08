using Foundation.Base.Domain.Implementation;

namespace HealthPlanSuite.Quote.Domain.Implementation
{
    /// <summary>
    /// Preço de um plano por faixa etária com controle de vigência
    /// </summary>
    public class PrecoPlano : Entity
    {
        public int PlanoId { get; set; }
        public int FaixaEtariaId { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataInicioVigencia { get; set; }
        public DateTime? DataFimVigencia { get; set; }
        public bool Ativo { get; set; } = true;

        // Navigation properties
        public virtual Plano Plano { get; set; } = null!;
        public virtual FaixaEtaria FaixaEtaria { get; set; } = null!;
    }
}