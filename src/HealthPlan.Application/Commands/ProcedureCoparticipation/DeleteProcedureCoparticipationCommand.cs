using MediatR;

namespace HealthPlan.Application.Commands
{
    public class DeleteProcedureCoparticipationCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
