using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthPlanSuite.Core.Domain.Implementation
{
    [Table("Operadora")]
    public class Operadora
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [StringLength(18)]
        public string CNPJ { get; set; } = string.Empty;

        [StringLength(255)]
        public string? Site { get; set; }

        [StringLength(20)]
        public string? Telefone { get; set; }

        [Required]
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime DataAtualizacao { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual ICollection<Plano> Planos { get; set; } = new List<Plano>();
    }
}