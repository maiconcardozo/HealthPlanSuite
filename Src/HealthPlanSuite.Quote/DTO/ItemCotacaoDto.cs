namespace HealthPlanSuite.Quote.DTO
{
    /// <summary>
    /// DTO para transferência de dados do Item de Cotação
    /// </summary>
    public class ItemCotacaoDto
    {
        public int Id { get; set; }
        public int CotacaoId { get; set; }
        public int PlanoId { get; set; }
        public string? PlanoNome { get; set; }
        public string? PlanoOperadora { get; set; }
        public int QuantidadeTitulares { get; set; } = 1;
        public int QuantidadeDependentes { get; set; } = 0;
        public decimal ValorTitular { get; set; }
        public decimal ValorDependentes { get; set; } = 0.00m;
        public decimal ValorTotal { get; set; }
        public bool Selecionado { get; set; } = false;
        public string? ObservacoesItem { get; set; }
    }
}