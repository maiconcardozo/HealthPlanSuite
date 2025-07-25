using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthPlanSuite.Core.Domain.Implementation
{
    [Table("Reajuste")]
    public class Reajuste
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int PlanoId { get; set; }

        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal Percentual { get; set; }

        [Required]
        public DateTime DataReajuste { get; set; }

        [Required]
        [StringLength(50)]
        public string TipoReajuste { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Observacao { get; set; }

        [Required]
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime DataAtualizacao { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("PlanoId")]
        public virtual Plano Plano { get; set; } = null!;
    }
}