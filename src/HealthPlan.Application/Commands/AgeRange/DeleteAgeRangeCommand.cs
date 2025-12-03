using MediatR;

namespace HealthPlan.Application.Commands
{
    /// <summary>
    /// Command to delete an age range.
    /// </summary>
    public class DeleteAgeRangeCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
