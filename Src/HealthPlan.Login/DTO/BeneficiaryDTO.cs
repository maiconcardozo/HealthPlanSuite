namespace HealthPlan.Login.DTO;

public class CreateBeneficiaryRequestDTO
{
    public string Name { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string Relationship { get; set; } = string.Empty;
    public int HealthPlanId { get; set; }
}

public class BeneficiaryResponseDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string Relationship { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
    public int HealthPlanId { get; set; }
    public string HealthPlanName { get; set; } = string.Empty;
}