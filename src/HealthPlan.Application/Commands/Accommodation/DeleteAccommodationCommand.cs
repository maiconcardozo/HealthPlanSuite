using MediatR;

namespace HealthPlan.Application.Commands
{
    /// <summary>
    /// Command to delete an accommodation.
    /// </summary>
    public class DeleteAccommodationCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
