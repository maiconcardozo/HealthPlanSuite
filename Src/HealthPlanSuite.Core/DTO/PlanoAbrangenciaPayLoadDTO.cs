using System.ComponentModel.DataAnnotations;

namespace HealthPlanSuite.Core.DTO
{
    public class PlanoAbrangenciaPayLoadDTO
    {
        [Required(ErrorMessage = "PlanoId é obrigatório")]
        public int PlanoId { get; set; }

        [Required(ErrorMessage = "EstabelecimentoId é obrigatório")]
        public int EstabelecimentoId { get; set; }
    }
}