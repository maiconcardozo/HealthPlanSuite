namespace HealthPlanSuite.Quote.DTO
{
    /// <summary>
    /// DTO para transferência de dados da Operadora
    /// </summary>
    public class OperadoraDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string RegistroANS { get; set; } = string.Empty;
        public string CNPJ { get; set; } = string.Empty;
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public string? Site { get; set; }
        public bool Ativa { get; set; } = true;
    }

    /// <summary>
    /// DTO para criação/atualização de Operadora
    /// </summary>
    public class OperadoraCreateDto
    {
        public string Nome { get; set; } = string.Empty;
        public string RegistroANS { get; set; } = string.Empty;
        public string CNPJ { get; set; } = string.Empty;
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public string? Site { get; set; }
    }
}