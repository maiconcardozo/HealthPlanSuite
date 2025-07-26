using Foundation.Base.Domain.Implementation;

namespace HealthPlanSuite.Domain.Implementation
{
    public class EstabelecimentoSaude : Entity
    {
        public string Nome { get; private set; }
        public string Tipo { get; private set; }
        public string Endereco { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        
        // Relacionamentos
        public virtual ICollection<PlanoAbrangencia> PlanosAbrangencia { get; set; }
        
        // Construtor privado para EF Core
        private EstabelecimentoSaude() 
        {
            PlanosAbrangencia = new List<PlanoAbrangencia>();
        }
        
        // Construtor público com validações
        public EstabelecimentoSaude(string nome, string tipo, string endereco, string cidade, string estado) : this()
        {
            SetNome(nome);
            SetTipo(tipo);
            SetEndereco(endereco);
            SetCidade(cidade);
            SetEstado(estado);
        }
        
        // Métodos de negócio
        public void SetNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome não pode ser vazio");
                
            if (nome.Length > 200)
                throw new ArgumentException("Nome não pode ter mais de 200 caracteres");
                
            Nome = nome;
            MarkAsUpdated();
        }
        
        public void SetTipo(string tipo)
        {
            if (string.IsNullOrWhiteSpace(tipo))
                throw new ArgumentException("Tipo não pode ser vazio");
                
            var tiposValidos = new[] { "Clínica", "Hospital", "Laboratório", "Centro de Diagnóstico", "Pronto Socorro" };
            
            if (!tiposValidos.Contains(tipo))
                throw new ArgumentException($"Tipo deve ser um dos seguintes: {string.Join(", ", tiposValidos)}");
                
            Tipo = tipo;
            MarkAsUpdated();
        }
        
        public void SetEndereco(string endereco)
        {
            if (string.IsNullOrWhiteSpace(endereco))
                throw new ArgumentException("Endereço não pode ser vazio");
                
            if (endereco.Length > 500)
                throw new ArgumentException("Endereço não pode ter mais de 500 caracteres");
                
            Endereco = endereco;
            MarkAsUpdated();
        }
        
        public void SetCidade(string cidade)
        {
            if (string.IsNullOrWhiteSpace(cidade))
                throw new ArgumentException("Cidade não pode ser vazia");
                
            if (cidade.Length > 100)
                throw new ArgumentException("Cidade não pode ter mais de 100 caracteres");
                
            Cidade = cidade;
            MarkAsUpdated();
        }
        
        public void SetEstado(string estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
                throw new ArgumentException("Estado não pode ser vazio");
                
            if (estado.Length != 2)
                throw new ArgumentException("Estado deve ter exatamente 2 caracteres (sigla do estado)");
                
            Estado = estado.ToUpper();
            MarkAsUpdated();
        }
        
        // Métodos de validação de negócio
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Nome) && 
                   !string.IsNullOrWhiteSpace(Tipo) &&
                   !string.IsNullOrWhiteSpace(Endereco) &&
                   !string.IsNullOrWhiteSpace(Cidade) &&
                   !string.IsNullOrWhiteSpace(Estado);
        }
    }
}