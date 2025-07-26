using Foundation.Base.Domain.Implementation;

namespace HealthPlanSuite.Domain.Implementation
{
    public class TabelaPreco : Entity
    {
        public int PlanoId { get; private set; }
        public int FaixaEtariaId { get; private set; }
        public decimal Mensalidade { get; private set; }
        public decimal? ValorCoparticipacao { get; private set; }
        public DateTime DataInicioVigencia { get; private set; }
        public DateTime? DataFimVigencia { get; private set; }
        
        // Relacionamentos
        public virtual Plano Plano { get; set; }
        public virtual FaixaEtaria FaixaEtaria { get; set; }
        
        // Construtor privado para EF Core
        private TabelaPreco() { }
        
        // Construtor público com validações
        public TabelaPreco(int planoId, int faixaEtariaId, decimal mensalidade, DateTime dataInicioVigencia, 
                          decimal? valorCoparticipacao = null, DateTime? dataFimVigencia = null)
        {
            SetPlanoId(planoId);
            SetFaixaEtariaId(faixaEtariaId);
            SetMensalidade(mensalidade);
            SetValorCoparticipacao(valorCoparticipacao);
            SetVigencia(dataInicioVigencia, dataFimVigencia);
        }
        
        // Métodos de negócio
        public void SetPlanoId(int planoId)
        {
            if (planoId <= 0)
                throw new ArgumentException("ID do plano deve ser maior que zero");
                
            PlanoId = planoId;
            MarkAsUpdated();
        }
        
        public void SetFaixaEtariaId(int faixaEtariaId)
        {
            if (faixaEtariaId <= 0)
                throw new ArgumentException("ID da faixa etária deve ser maior que zero");
                
            FaixaEtariaId = faixaEtariaId;
            MarkAsUpdated();
        }
        
        public void SetMensalidade(decimal mensalidade)
        {
            if (mensalidade <= 0)
                throw new ArgumentException("Mensalidade deve ser maior que zero");
                
            if (mensalidade > 999999.99m)
                throw new ArgumentException("Mensalidade não pode exceder R$ 999.999,99");
                
            Mensalidade = mensalidade;
            MarkAsUpdated();
        }
        
        public void SetValorCoparticipacao(decimal? valorCoparticipacao)
        {
            if (valorCoparticipacao.HasValue)
            {
                if (valorCoparticipacao.Value < 0)
                    throw new ArgumentException("Valor de coparticipação não pode ser negativo");
                    
                if (valorCoparticipacao.Value > 999999.99m)
                    throw new ArgumentException("Valor de coparticipação não pode exceder R$ 999.999,99");
            }
                
            ValorCoparticipacao = valorCoparticipacao;
            MarkAsUpdated();
        }
        
        public void SetVigencia(DateTime dataInicioVigencia, DateTime? dataFimVigencia = null)
        {
            if (dataInicioVigencia == default(DateTime))
                throw new ArgumentException("Data de início de vigência é obrigatória");
                
            if (dataFimVigencia.HasValue && dataFimVigencia.Value <= dataInicioVigencia)
                throw new ArgumentException("Data de fim de vigência deve ser posterior à data de início");
                
            DataInicioVigencia = dataInicioVigencia;
            DataFimVigencia = dataFimVigencia;
            MarkAsUpdated();
        }
        
        // Métodos de validação de negócio
        public bool IsValid()
        {
            return PlanoId > 0 && 
                   FaixaEtariaId > 0 &&
                   Mensalidade > 0 && 
                   DataInicioVigencia != default(DateTime) &&
                   (!DataFimVigencia.HasValue || DataFimVigencia.Value > DataInicioVigencia);
        }
        
        public bool EstaVigente(DateTime? dataReferencia = null)
        {
            var data = dataReferencia ?? DateTime.UtcNow;
            return DataInicioVigencia <= data && (!DataFimVigencia.HasValue || DataFimVigencia.Value >= data);
        }
        
        public decimal CalcularValorTotal(bool aplicarCoparticipacao = false)
        {
            var valor = Mensalidade;
            
            if (aplicarCoparticipacao && ValorCoparticipacao.HasValue)
            {
                valor += ValorCoparticipacao.Value;
            }
            
            return valor;
        }
    }
}