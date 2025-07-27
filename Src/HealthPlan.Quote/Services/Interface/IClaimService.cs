using Authentication.Login.Domain.Implementation;
using System.Collections.Generic;

namespace Authentication.Login.Services.Interface
{
    public interface IClaimService
    {
        IEnumerable<Claim> GetAll();
        Claim? GetById(int id);
        Claim? GetByValue(string value);
        void AddClaim(Claim claim);
        void UpdateClaim(Claim claim);
        void DeleteClaim(int id);
    }
}