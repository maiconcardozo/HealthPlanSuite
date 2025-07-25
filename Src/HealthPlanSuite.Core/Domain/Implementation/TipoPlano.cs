using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthPlanSuite.Core.Domain.Implementation
{
    [Table("TipoPlano")]
    public class TipoPlano
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Descricao { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string RegulacaoANS { get; set; } = string.Empty;

        [Required]
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime DataAtualizacao { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual ICollection<Plano> Planos { get; set; } = new List<Plano>();
    }
}