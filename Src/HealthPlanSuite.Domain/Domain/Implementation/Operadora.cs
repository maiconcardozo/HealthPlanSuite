using Foundation.Base.Domain.Implementation;

namespace HealthPlanSuite.Domain.Implementation
{
    public class Operadora : Entity
    {
        public string Nome { get; private set; }
        public string CNPJ { get; private set; }
        public string? Site { get; private set; }
        public string? Telefone { get; private set; }
        
        // Relacionamentos
        public virtual ICollection<Plano> Planos { get; set; }
        
        // Construtor privado para EF Core
        private Operadora() 
        {
            Planos = new List<Plano>();
        }
        
        // Construtor público com validações
        public Operadora(string nome, string cnpj, string? site = null, string? telefone = null) : this()
        {
            SetNome(nome);
            SetCNPJ(cnpj);
            SetSite(site);
            SetTelefone(telefone);
        }
        
        // Métodos de negócio
        public void SetNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome não pode ser vazio");
                
            if (nome.Length > 100)
                throw new ArgumentException("Nome não pode ter mais de 100 caracteres");
                
            Nome = nome;
            MarkAsUpdated();
        }
        
        public void SetCNPJ(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
                throw new ArgumentException("CNPJ não pode ser vazio");
                
            // Remove caracteres especiais para validação
            var cnpjNumeros = cnpj.Replace(".", "").Replace("/", "").Replace("-", "");
            
            if (cnpjNumeros.Length != 14 || !cnpjNumeros.All(char.IsDigit))
                throw new ArgumentException("CNPJ deve ter 14 dígitos");
                
            CNPJ = cnpj;
            MarkAsUpdated();
        }
        
        public void SetSite(string? site)
        {
            if (!string.IsNullOrWhiteSpace(site) && site.Length > 255)
                throw new ArgumentException("Site não pode ter mais de 255 caracteres");
                
            Site = site;
            MarkAsUpdated();
        }
        
        public void SetTelefone(string? telefone)
        {
            if (!string.IsNullOrWhiteSpace(telefone) && telefone.Length > 20)
                throw new ArgumentException("Telefone não pode ter mais de 20 caracteres");
                
            Telefone = telefone;
            MarkAsUpdated();
        }
        
        // Métodos de validação de negócio
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Nome) && !string.IsNullOrWhiteSpace(CNPJ);
        }
    }
}