using System.ComponentModel.DataAnnotations;

namespace HealthPlanSuite.Core.DTO
{
    public class PlanoPayLoadDTO
    {
        [Required(ErrorMessage = "OperadoraId é obrigatório")]
        public int OperadoraId { get; set; }

        [Required(ErrorMessage = "TipoPlanoId é obrigatório")]
        public int TipoPlanoId { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(200, ErrorMessage = "Nome deve ter no máximo 200 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Cobertura deve ter no máximo 500 caracteres")]
        public string? Cobertura { get; set; }

        [Required(ErrorMessage = "PossuiCoparticipacao é obrigatório")]
        public bool PossuiCoparticipacao { get; set; }
    }
}