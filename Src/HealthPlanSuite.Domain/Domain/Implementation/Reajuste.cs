using Foundation.Base.Domain.Implementation;

namespace HealthPlanSuite.Domain.Implementation
{
    public class Reajuste : Entity
    {
        public int PlanoId { get; private set; }
        public decimal Percentual { get; private set; }
        public DateTime DataReajuste { get; private set; }
        public string TipoReajuste { get; private set; }
        public string? Observacao { get; private set; }
        
        // Relacionamentos
        public virtual Plano Plano { get; set; }
        
        // Construtor privado para EF Core
        private Reajuste() { }
        
        // Construtor público com validações
        public Reajuste(int planoId, decimal percentual, DateTime dataReajuste, string tipoReajuste, string? observacao = null)
        {
            SetPlanoId(planoId);
            SetPercentual(percentual);
            SetDataReajuste(dataReajuste);
            SetTipoReajuste(tipoReajuste);
            SetObservacao(observacao);
        }
        
        // Métodos de negócio
        public void SetPlanoId(int planoId)
        {
            if (planoId <= 0)
                throw new ArgumentException("ID do plano deve ser maior que zero");
                
            PlanoId = planoId;
            MarkAsUpdated();
        }
        
        public void SetPercentual(decimal percentual)
        {
            if (percentual < -100)
                throw new ArgumentException("Percentual não pode ser menor que -100%");
                
            if (percentual > 1000)
                throw new ArgumentException("Percentual não pode ser maior que 1000%");
                
            Percentual = percentual;
            MarkAsUpdated();
        }
        
        public void SetDataReajuste(DateTime dataReajuste)
        {
            if (dataReajuste == default(DateTime))
                throw new ArgumentException("Data do reajuste é obrigatória");
                
            DataReajuste = dataReajuste;
            MarkAsUpdated();
        }
        
        public void SetTipoReajuste(string tipoReajuste)
        {
            if (string.IsNullOrWhiteSpace(tipoReajuste))
                throw new ArgumentException("Tipo de reajuste não pode ser vazio");
                
            var tiposValidos = new[] { "ANS", "Aniversário", "Inflação", "Custos Médicos", "Especial", "Redução" };
            
            if (!tiposValidos.Contains(tipoReajuste))
                throw new ArgumentException($"Tipo de reajuste deve ser um dos seguintes: {string.Join(", ", tiposValidos)}");
                
            TipoReajuste = tipoReajuste;
            MarkAsUpdated();
        }
        
        public void SetObservacao(string? observacao)
        {
            if (!string.IsNullOrWhiteSpace(observacao) && observacao.Length > 500)
                throw new ArgumentException("Observação não pode ter mais de 500 caracteres");
                
            Observacao = observacao;
            MarkAsUpdated();
        }
        
        // Métodos de validação de negócio
        public bool IsValid()
        {
            return PlanoId > 0 && 
                   Percentual >= -100 && 
                   Percentual <= 1000 &&
                   DataReajuste != default(DateTime) &&
                   !string.IsNullOrWhiteSpace(TipoReajuste);
        }
        
        // Métodos de negócio específicos
        public decimal CalcularNovoValor(decimal valorAtual)
        {
            if (valorAtual <= 0)
                throw new ArgumentException("Valor atual deve ser maior que zero");
                
            var fatorReajuste = 1 + (Percentual / 100);
            return Math.Round(valorAtual * fatorReajuste, 2);
        }
        
        public bool IsReajustePositivo()
        {
            return Percentual > 0;
        }
        
        public bool IsReajusteNegativo()
        {
            return Percentual < 0;
        }
        
        public string GetDescricaoReajuste()
        {
            var tipo = IsReajustePositivo() ? "Aumento" : "Redução";
            var percentualAbsoluto = Math.Abs(Percentual);
            return $"{tipo} de {percentualAbsoluto:F2}% - {TipoReajuste}";
        }
    }
}