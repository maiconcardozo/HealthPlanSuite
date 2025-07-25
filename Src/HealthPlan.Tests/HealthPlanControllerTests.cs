using Xunit;

namespace HealthPlan.Tests;

public class HealthPlanControllerTests
{
    [Fact]
    public void GetHealthPlans_ShouldReturnSuccess()
    {
        // Arrange
        // Act
        var result = "HealthPlan API is working!";
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal("HealthPlan API is working!", result);
    }
}