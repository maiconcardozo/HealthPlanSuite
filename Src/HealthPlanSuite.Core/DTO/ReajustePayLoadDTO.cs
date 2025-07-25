using System.ComponentModel.DataAnnotations;

namespace HealthPlanSuite.Core.DTO
{
    public class ReajustePayLoadDTO
    {
        [Required(ErrorMessage = "PlanoId é obrigatório")]
        public int PlanoId { get; set; }

        [Required(ErrorMessage = "Percentual é obrigatório")]
        [Range(0.01, 100.00, ErrorMessage = "Percentual deve estar entre 0.01 e 100.00")]
        public decimal Percentual { get; set; }

        [Required(ErrorMessage = "Data do reajuste é obrigatória")]
        public DateTime DataReajuste { get; set; }

        [Required(ErrorMessage = "Tipo de reajuste é obrigatório")]
        [StringLength(50, ErrorMessage = "Tipo de reajuste deve ter no máximo 50 caracteres")]
        public string TipoReajuste { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Observação deve ter no máximo 500 caracteres")]
        public string? Observacao { get; set; }
    }
}