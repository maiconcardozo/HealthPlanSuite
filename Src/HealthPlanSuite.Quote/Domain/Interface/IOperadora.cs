namespace HealthPlanSuite.Quote.Domain.Interface
{
    /// <summary>
    /// Interface para Operadora de plano de saúde
    /// </summary>
    public interface IOperadora
    {
        string Nome { get; set; }
        string RegistroANS { get; set; }
        string CNPJ { get; set; }
        string? Telefone { get; set; }
        string? Email { get; set; }
        string? Site { get; set; }
        bool Ativa { get; set; }
    }
}