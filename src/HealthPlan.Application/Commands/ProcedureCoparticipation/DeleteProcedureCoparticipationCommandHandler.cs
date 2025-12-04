using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Commands
{
    public class DeleteProcedureCoparticipationCommandHandler : IRequestHandler<DeleteProcedureCoparticipationCommand, bool>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public DeleteProcedureCoparticipationCommandHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(DeleteProcedureCoparticipationCommand request, CancellationToken cancellationToken)
        {
            var procedureCoparticipation = unitOfWork.ProcedureCoparticipationRepository.GetById(request.Id);

            if (procedureCoparticipation == null)
            {
                return Task.FromResult(false);
            }

            unitOfWork.ProcedureCoparticipationRepository.Remove(procedureCoparticipation);

            return Task.FromResult(true);
        }
    }
}
