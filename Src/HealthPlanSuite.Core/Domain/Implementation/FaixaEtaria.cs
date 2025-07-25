using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthPlanSuite.Core.Domain.Implementation
{
    [Table("FaixaEtaria")]
    public class FaixaEtaria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Descricao { get; set; } = string.Empty;

        [Required]
        public int IdadeMin { get; set; }

        [Required]
        public int IdadeMax { get; set; }

        [Required]
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime DataAtualizacao { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual ICollection<TabelaPreco> TabelasPreco { get; set; } = new List<TabelaPreco>();
    }
}