using Authentication.Login.Domain.Implementation;
using Authentication.Login.Repository.Interface;
using Authentication.Login.Services.Interface;
using System.Collections.Generic;

namespace Authentication.Login.Services.Implementation
{
    public class ClaimService : IClaimService
    {
        private readonly IClaimRepository _claimRepository;

        public ClaimService(IClaimRepository claimRepository)
        {
            _claimRepository = claimRepository;
        }

        public IEnumerable<Claim> GetAll() => _claimRepository.GetAll();

        public Claim? GetById(int id) => _claimRepository.GetById(id);

        public Claim? GetByValue(string value) => _claimRepository.GetByValue(value);

        public void AddClaim(Claim claim) => _claimRepository.Add(claim);

        public void UpdateClaim(Claim claim) => _claimRepository.Update(claim);

        public void DeleteClaim(int id)
        {
            var claim = _claimRepository.GetById(id);
            if (claim != null)
                _claimRepository.Remove(claim);
        }
    }
}