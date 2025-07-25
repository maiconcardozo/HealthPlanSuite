using System.ComponentModel.DataAnnotations;

namespace HealthPlanSuite.Core.DTO
{
    public class FaixaEtariaPayLoadDTO
    {
        [Required(ErrorMessage = "Descrição é obrigatória")]
        [StringLength(100, ErrorMessage = "Descrição deve ter no máximo 100 caracteres")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "Idade mínima é obrigatória")]
        [Range(0, 120, ErrorMessage = "Idade mínima deve estar entre 0 e 120 anos")]
        public int IdadeMin { get; set; }

        [Required(ErrorMessage = "Idade máxima é obrigatória")]
        [Range(0, 120, ErrorMessage = "Idade máxima deve estar entre 0 e 120 anos")]
        public int IdadeMax { get; set; }
    }
}