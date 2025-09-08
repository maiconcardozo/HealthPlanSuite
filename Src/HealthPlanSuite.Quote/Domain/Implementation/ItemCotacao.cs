using Foundation.Base.Domain.Implementation;

namespace HealthPlanSuite.Quote.Domain.Implementation
{
    /// <summary>
    /// Item da cotação representando um plano cotado com valores calculados
    /// </summary>
    public class ItemCotacao : Entity
    {
        public int CotacaoId { get; set; }
        public int PlanoId { get; set; }
        public int QuantidadeTitulares { get; set; } = 1;
        public int QuantidadeDependentes { get; set; } = 0;
        public decimal ValorTitular { get; set; }
        public decimal ValorDependentes { get; set; } = 0.00m;
        public decimal ValorTotal { get; set; }
        public bool Selecionado { get; set; } = false;
        public string? ObservacoesItem { get; set; }

        // Navigation properties
        public virtual Cotacao Cotacao { get; set; } = null!;
        public virtual Plano Plano { get; set; } = null!;
    }
}