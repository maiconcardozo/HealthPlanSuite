using HealthPlanSuite.Quote.Domain.Implementation;

namespace HealthPlanSuite.Quote.DTO
{
    /// <summary>
    /// DTO para transferência de dados do Plano
    /// </summary>
    public class PlanoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public int OperadoraId { get; set; }
        public string? OperadoraNome { get; set; }
        public int TipoPlanoId { get; set; }
        public string? TipoPlanoNome { get; set; }
        public string? Descricao { get; set; }
        public AbrangenciaGeografica AbrangenciaGeografica { get; set; }
        public TipoContratacao TipoContratacao { get; set; }
        public int IdadeMinima { get; set; } = 0;
        public int IdadeMaxima { get; set; } = 99;
        public bool Ativo { get; set; } = true;
    }

    /// <summary>
    /// DTO para criação/atualização de Plano
    /// </summary>
    public class PlanoCreateDto
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
    }
}