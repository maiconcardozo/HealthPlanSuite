using System.ComponentModel.DataAnnotations;

namespace HealthPlanSuite.Core.DTO
{
    public class TabelaPrecoPayLoadDTO
    {
        [Required(ErrorMessage = "PlanoId é obrigatório")]
        public int PlanoId { get; set; }

        [Required(ErrorMessage = "FaixaEtariaId é obrigatório")]
        public int FaixaEtariaId { get; set; }

        [Required(ErrorMessage = "Mensalidade é obrigatória")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Mensalidade deve ser maior que zero")]
        public decimal Mensalidade { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Valor de coparticipação deve ser maior ou igual a zero")]
        public decimal? ValorCoparticipacao { get; set; }

        [Required(ErrorMessage = "Data de início de vigência é obrigatória")]
        public DateTime DataInicioVigencia { get; set; }

        public DateTime? DataFimVigencia { get; set; }
    }
}