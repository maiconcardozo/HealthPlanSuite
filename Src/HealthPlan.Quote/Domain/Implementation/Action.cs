using Authentication.Login.Domain.Interface;
using Foundation.Base.Domain.Implementation;

namespace Authentication.Login.Domain.Implementation
{
    public class Action : Entity, IAction
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<ClaimAction> LstClaimAction { get; set; } = new List<ClaimAction>();

        ICollection<IClaimAction> IAction.LstClaimAction
        {
            get => (ICollection<IClaimAction>)LstClaimAction;
            set => LstClaimAction = (ICollection<ClaimAction>)value;
        }
    }
}