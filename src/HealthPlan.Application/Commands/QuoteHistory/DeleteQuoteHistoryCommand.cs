using MediatR;

namespace HealthPlan.Application.Commands
{
    public class DeleteQuoteHistoryCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
