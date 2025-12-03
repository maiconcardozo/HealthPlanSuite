using MediatR;

namespace HealthPlan.Application.Commands
{
    /// <summary>
    /// Command to delete a quote.
    /// </summary>
    public class DeleteQuoteCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
