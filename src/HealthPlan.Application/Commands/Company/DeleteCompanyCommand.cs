using MediatR;

namespace HealthPlan.Application.Commands
{
    /// <summary>
    /// Command to delete a company.
    /// </summary>
    public class DeleteCompanyCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
