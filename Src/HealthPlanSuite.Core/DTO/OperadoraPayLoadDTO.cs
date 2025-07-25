using System.ComponentModel.DataAnnotations;

namespace HealthPlanSuite.Core.DTO
{
    public class OperadoraPayLoadDTO
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(200, ErrorMessage = "Nome deve ter no máximo 200 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "CNPJ é obrigatório")]
        [StringLength(18, MinimumLength = 14, ErrorMessage = "CNPJ deve ter entre 14 e 18 caracteres")]
        public string CNPJ { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "Site deve ter no máximo 255 caracteres")]
        public string? Site { get; set; }

        [StringLength(20, ErrorMessage = "Telefone deve ter no máximo 20 caracteres")]
        public string? Telefone { get; set; }
    }
}