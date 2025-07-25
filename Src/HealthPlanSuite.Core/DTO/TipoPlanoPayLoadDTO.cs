using System.ComponentModel.DataAnnotations;

namespace HealthPlanSuite.Core.DTO
{
    public class TipoPlanoPayLoadDTO
    {
        [Required(ErrorMessage = "Descrição é obrigatória")]
        [StringLength(100, ErrorMessage = "Descrição deve ter no máximo 100 caracteres")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "Regulação ANS é obrigatória")]
        [StringLength(50, ErrorMessage = "Regulação ANS deve ter no máximo 50 caracteres")]
        public string RegulacaoANS { get; set; } = string.Empty;
    }
}