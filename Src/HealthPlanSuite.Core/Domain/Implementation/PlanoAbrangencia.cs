using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthPlanSuite.Core.Domain.Implementation
{
    [Table("PlanoAbrangencia")]
    public class PlanoAbrangencia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int PlanoId { get; set; }

        [Required]
        public int EstabelecimentoId { get; set; }

        [Required]
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime DataAtualizacao { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("PlanoId")]
        public virtual Plano Plano { get; set; } = null!;

        [ForeignKey("EstabelecimentoId")]
        public virtual EstabelecimentoSaude EstabelecimentoSaude { get; set; } = null!;
    }
}