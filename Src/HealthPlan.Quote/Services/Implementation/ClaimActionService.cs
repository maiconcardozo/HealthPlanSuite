using Authentication.Login.Domain.Implementation;
using Authentication.Login.Repository.Interface;
using Authentication.Login.Services.Interface;
using System.Collections.Generic;

namespace Authentication.Login.Services.Implementation
{
    public class ClaimActionService : IClaimActionService
    {
        private readonly IClaimActionRepository _claimActionRepository;

        public ClaimActionService(IClaimActionRepository claimActionRepository)
        {
            _claimActionRepository = claimActionRepository;
        }

        public IEnumerable<ClaimAction> GetAll() => _claimActionRepository.GetAll();

        public ClaimAction? GetById(int id) => _claimActionRepository.GetById(id);

        public ClaimAction? GetByClaimAndAction(int idClaim, int idAction) =>
            _claimActionRepository.GetByClaimAndAction(idClaim, idAction);

        public IEnumerable<ClaimAction> GetByClaimId(int idClaim) =>
            _claimActionRepository.GetByClaimId(idClaim);

        public IEnumerable<ClaimAction> GetByActionId(int idAction) =>
            _claimActionRepository.GetByActionId(idAction);

        public void AddClaimAction(ClaimAction claimAction) =>
            _claimActionRepository.Add(claimAction);

        public void UpdateClaimAction(ClaimAction claimAction) =>
            _claimActionRepository.Update(claimAction);

        public void DeleteClaimAction(int id)
        {
            var entity = _claimActionRepository.GetById(id);
            if (entity != null)
                _claimActionRepository.Remove(entity);
        }
    }
}