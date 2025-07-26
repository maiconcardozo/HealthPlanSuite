using Foundation.Base.Domain.Implementation;

namespace HealthPlanSuite.Domain.Implementation
{
    public class PlanoAbrangencia : Entity
    {
        public int PlanoId { get; private set; }
        public int EstabelecimentoId { get; private set; }
        
        // Relacionamentos
        public virtual Plano Plano { get; set; }
        public virtual EstabelecimentoSaude EstabelecimentoSaude { get; set; }
        
        // Construtor privado para EF Core
        private PlanoAbrangencia() { }
        
        // Construtor público com validações
        public PlanoAbrangencia(int planoId, int estabelecimentoId)
        {
            SetPlanoId(planoId);
            SetEstabelecimentoId(estabelecimentoId);
        }
        
        // Métodos de negócio
        public void SetPlanoId(int planoId)
        {
            if (planoId <= 0)
                throw new ArgumentException("ID do plano deve ser maior que zero");
                
            PlanoId = planoId;
            MarkAsUpdated();
        }
        
        public void SetEstabelecimentoId(int estabelecimentoId)
        {
            if (estabelecimentoId <= 0)
                throw new ArgumentException("ID do estabelecimento deve ser maior que zero");
                
            EstabelecimentoId = estabelecimentoId;
            MarkAsUpdated();
        }
        
        // Métodos de validação de negócio
        public bool IsValid()
        {
            return PlanoId > 0 && EstabelecimentoId > 0;
        }
    }
}