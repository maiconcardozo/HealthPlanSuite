using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthPlanSuite.Core.Domain.Implementation
{
    [Table("Plano")]
    public class Plano
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int OperadoraId { get; set; }

        [Required]
        public int TipoPlanoId { get; set; }

        [Required]
        [StringLength(200)]
        public string Nome { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Cobertura { get; set; }

        [Required]
        public bool PossuiCoparticipacao { get; set; }

        [Required]
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime DataAtualizacao { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("OperadoraId")]
        public virtual Operadora Operadora { get; set; } = null!;

        [ForeignKey("TipoPlanoId")]
        public virtual TipoPlano TipoPlano { get; set; } = null!;

        public virtual ICollection<TabelaPreco> TabelasPreco { get; set; } = new List<TabelaPreco>();
        public virtual ICollection<Reajuste> Reajustes { get; set; } = new List<Reajuste>();
        public virtual ICollection<PlanoAbrangencia> PlanoAbrangencias { get; set; } = new List<PlanoAbrangencia>();
    }
}