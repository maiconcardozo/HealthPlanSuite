using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthPlanSuite.Core.Domain.Implementation
{
    [Table("EstabelecimentoSaude")]
    public class EstabelecimentoSaude
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Tipo { get; set; } = string.Empty; // ex: Clínica, Hospital, Laboratório

        [StringLength(300)]
        public string? Endereco { get; set; }

        [StringLength(100)]
        public string? Cidade { get; set; }

        [StringLength(2)]
        public string? Estado { get; set; }

        [Required]
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime DataAtualizacao { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual ICollection<PlanoAbrangencia> PlanoAbrangencias { get; set; } = new List<PlanoAbrangencia>();
    }
}