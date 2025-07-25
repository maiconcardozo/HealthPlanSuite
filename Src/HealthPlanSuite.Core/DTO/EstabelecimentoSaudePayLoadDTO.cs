using System.ComponentModel.DataAnnotations;

namespace HealthPlanSuite.Core.DTO
{
    public class EstabelecimentoSaudePayLoadDTO
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(200, ErrorMessage = "Nome deve ter no máximo 200 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tipo é obrigatório")]
        [StringLength(50, ErrorMessage = "Tipo deve ter no máximo 50 caracteres")]
        public string Tipo { get; set; } = string.Empty;

        [StringLength(300, ErrorMessage = "Endereço deve ter no máximo 300 caracteres")]
        public string? Endereco { get; set; }

        [StringLength(100, ErrorMessage = "Cidade deve ter no máximo 100 caracteres")]
        public string? Cidade { get; set; }

        [StringLength(2, MinimumLength = 2, ErrorMessage = "Estado deve ter exatamente 2 caracteres")]
        public string? Estado { get; set; }
    }
}