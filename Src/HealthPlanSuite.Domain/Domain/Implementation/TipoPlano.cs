using Foundation.Base.Domain.Implementation;

namespace HealthPlanSuite.Domain.Implementation
{
    public class TipoPlano : Entity
    {
        public string Descricao { get; private set; }
        public string? RegulacaoANS { get; private set; }
        
        // Relacionamentos
        public virtual ICollection<Plano> Planos { get; set; }
        
        // Construtor privado para EF Core
        private TipoPlano() 
        {
            Planos = new List<Plano>();
        }
        
        // Construtor público com validações
        public TipoPlano(string descricao, string? regulacaoANS = null) : this()
        {
            SetDescricao(descricao);
            SetRegulacaoANS(regulacaoANS);
        }
        
        // Métodos de negócio
        public void SetDescricao(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
                throw new ArgumentException("Descrição não pode ser vazia");
                
            if (descricao.Length > 255)
                throw new ArgumentException("Descrição não pode ter mais de 255 caracteres");
                
            Descricao = descricao;
            MarkAsUpdated();
        }
        
        public void SetRegulacaoANS(string? regulacaoANS)
        {
            if (!string.IsNullOrWhiteSpace(regulacaoANS) && regulacaoANS.Length > 500)
                throw new ArgumentException("Regulação ANS não pode ter mais de 500 caracteres");
                
            RegulacaoANS = regulacaoANS;
            MarkAsUpdated();
        }
        
        // Métodos de validação de negócio
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Descricao);
        }
    }
}