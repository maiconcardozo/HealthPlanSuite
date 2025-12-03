using MediatR;

namespace HealthPlan.Application.Commands
{
    /// <summary>
    /// Command to delete a beneficiary.
    /// </summary>
    public class DeleteBeneficiaryCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
