namespace HealthPlan.Login.Domain.Implementation;

public class HealthPlanEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Coverage { get; set; } = string.Empty;
    public decimal MonthlyFee { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
    
    // Navigation properties
    public ICollection<Beneficiary> Beneficiaries { get; set; } = new List<Beneficiary>();
}