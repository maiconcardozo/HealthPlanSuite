using Microsoft.AspNetCore.Mvc;

namespace HealthPlan.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthPlanController : ControllerBase
{
    private readonly ILogger<HealthPlanController> _logger;

    public HealthPlanController(ILogger<HealthPlanController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetHealthPlans()
    {
        return Ok("HealthPlan API is working!");
    }
}