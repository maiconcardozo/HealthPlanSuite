namespace HealthPlan.Login.Domain.Implementation;

public class Beneficiary
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public string Relationship { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
    
    // Foreign key
    public int HealthPlanId { get; set; }
    
    // Navigation property
    public HealthPlanEntity HealthPlan { get; set; } = null!;
}