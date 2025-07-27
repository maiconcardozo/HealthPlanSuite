using Authentication.Login.Domain.Interface;
using Authentication.Login.Enum;
using Foundation.Base.Domain.Implementation;

namespace Authentication.Login.Domain.Implementation
{
    public class Claim : Entity, IClaim
    {
        public ClaimType Type { get; set; }
        public string Value { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ICollection<ClaimAction> LstClaimAction { get; set; } = new List<ClaimAction>();

        ICollection<IClaimAction> IClaim.LstClaimAction
        {
            get => (ICollection<IClaimAction>)LstClaimAction;
            set => LstClaimAction = (ICollection<ClaimAction>)value;
        }
    }
}