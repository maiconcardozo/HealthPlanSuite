using Foundation.Base.Domain.Implementation;

namespace HealthPlanSuite.Domain.Implementation
{
    public class Plano : Entity
    {
        public int OperadoraId { get; private set; }
        public int TipoPlanoId { get; private set; }
        public string Nome { get; private set; }
        public string Cobertura { get; private set; }
        public bool PossuiCoparticipacao { get; private set; }
        
        // Relacionamentos
        public virtual Operadora Operadora { get; set; }
        public virtual TipoPlano TipoPlano { get; set; }
        public virtual ICollection<TabelaPreco> TabelasPreco { get; set; }
        public virtual ICollection<Reajuste> Reajustes { get; set; }
        public virtual ICollection<PlanoAbrangencia> PlanosAbrangencia { get; set; }
        
        // Construtor privado para EF Core
        private Plano() 
        {
            TabelasPreco = new List<TabelaPreco>();
            Reajustes = new List<Reajuste>();
            PlanosAbrangencia = new List<PlanoAbrangencia>();
        }
        
        // Construtor público com validações
        public Plano(int operadoraId, int tipoPlanoId, string nome, string cobertura, bool possuiCoparticipacao = false) : this()
        {
            SetOperadoraId(operadoraId);
            SetTipoPlanoId(tipoPlanoId);
            SetNome(nome);
            SetCobertura(cobertura);
            SetPossuiCoparticipacao(possuiCoparticipacao);
        }
        
        // Métodos de negócio
        public void SetOperadoraId(int operadoraId)
        {
            if (operadoraId <= 0)
                throw new ArgumentException("ID da operadora deve ser maior que zero");
                
            OperadoraId = operadoraId;
            MarkAsUpdated();
        }
        
        public void SetTipoPlanoId(int tipoPlanoId)
        {
            if (tipoPlanoId <= 0)
                throw new ArgumentException("ID do tipo de plano deve ser maior que zero");
                
            TipoPlanoId = tipoPlanoId;
            MarkAsUpdated();
        }
        
        public void SetNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome não pode ser vazio");
                
            if (nome.Length > 150)
                throw new ArgumentException("Nome não pode ter mais de 150 caracteres");
                
            Nome = nome;
            MarkAsUpdated();
        }
        
        public void SetCobertura(string cobertura)
        {
            if (string.IsNullOrWhiteSpace(cobertura))
                throw new ArgumentException("Cobertura não pode ser vazia");
                
            if (cobertura.Length > 1000)
                throw new ArgumentException("Cobertura não pode ter mais de 1000 caracteres");
                
            Cobertura = cobertura;
            MarkAsUpdated();
        }
        
        public void SetPossuiCoparticipacao(bool possuiCoparticipacao)
        {
            PossuiCoparticipacao = possuiCoparticipacao;
            MarkAsUpdated();
        }
        
        // Métodos de validação de negócio
        public bool IsValid()
        {
            return OperadoraId > 0 && 
                   TipoPlanoId > 0 &&
                   !string.IsNullOrWhiteSpace(Nome) && 
                   !string.IsNullOrWhiteSpace(Cobertura);
        }
        
        // Métodos de negócio específicos
        public bool TemCoberturaPara(EstabelecimentoSaude estabelecimento)
        {
            return PlanosAbrangencia.Any(pa => pa.EstabelecimentoId == estabelecimento.Id);
        }
        
        public TabelaPreco? ObterPrecoParaIdade(int idade, DateTime? dataReferencia = null)
        {
            var data = dataReferencia ?? DateTime.UtcNow;
            
            return TabelasPreco
                .Where(tp => tp.DataInicioVigencia <= data && 
                           (tp.DataFimVigencia == null || tp.DataFimVigencia >= data) &&
                           tp.FaixaEtaria.ContemIdade(idade))
                .OrderByDescending(tp => tp.DataInicioVigencia)
                .FirstOrDefault();
        }
    }
}