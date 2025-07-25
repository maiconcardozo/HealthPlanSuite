using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthPlanSuite.Core.Domain.Implementation
{
    [Table("TabelaPreco")]
    public class TabelaPreco
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int PlanoId { get; set; }

        [Required]
        public int FaixaEtariaId { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Mensalidade { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? ValorCoparticipacao { get; set; }

        [Required]
        public DateTime DataInicioVigencia { get; set; }

        public DateTime? DataFimVigencia { get; set; }

        [Required]
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime DataAtualizacao { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("PlanoId")]
        public virtual Plano Plano { get; set; } = null!;

        [ForeignKey("FaixaEtariaId")]
        public virtual FaixaEtaria FaixaEtaria { get; set; } = null!;
    }
}