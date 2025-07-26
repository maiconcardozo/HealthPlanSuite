using Foundation.Base.Domain.Implementation;

namespace HealthPlanSuite.Domain.Implementation
{
    public class FaixaEtaria : Entity
    {
        public string Descricao { get; private set; }
        public int IdadeMin { get; private set; }
        public int IdadeMax { get; private set; }
        
        // Relacionamentos
        public virtual ICollection<TabelaPreco> TabelasPreco { get; set; }
        
        // Construtor privado para EF Core
        private FaixaEtaria() 
        {
            TabelasPreco = new List<TabelaPreco>();
        }
        
        // Construtor público com validações
        public FaixaEtaria(string descricao, int idadeMin, int idadeMax) : this()
        {
            SetDescricao(descricao);
            SetIdades(idadeMin, idadeMax);
        }
        
        // Métodos de negócio
        public void SetDescricao(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
                throw new ArgumentException("Descrição não pode ser vazia");
                
            if (descricao.Length > 100)
                throw new ArgumentException("Descrição não pode ter mais de 100 caracteres");
                
            Descricao = descricao;
            MarkAsUpdated();
        }
        
        public void SetIdades(int idadeMin, int idadeMax)
        {
            if (idadeMin < 0)
                throw new ArgumentException("Idade mínima não pode ser negativa");
                
            if (idadeMax < 0)
                throw new ArgumentException("Idade máxima não pode ser negativa");
                
            if (idadeMin > idadeMax)
                throw new ArgumentException("Idade mínima não pode ser maior que idade máxima");
                
            if (idadeMax > 120)
                throw new ArgumentException("Idade máxima não pode ser maior que 120 anos");
                
            IdadeMin = idadeMin;
            IdadeMax = idadeMax;
            MarkAsUpdated();
        }
        
        // Métodos de validação de negócio
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Descricao) && 
                   IdadeMin >= 0 && 
                   IdadeMax >= 0 && 
                   IdadeMin <= IdadeMax;
        }
        
        public bool ContemIdade(int idade)
        {
            return idade >= IdadeMin && idade <= IdadeMax;
        }
    }
}