using SportBet.Contracts;
using SportBet.Models.Display;

namespace SportBet.CommonControls.Bets.ViewModels
{
    public class ClientBetListViewModel : BetListViewModel
    {
        public ClientBetListViewModel(ISubject subject, FacadeBase<BetDisplayModel> facade)
            : base(subject, facade) { }

        public override bool HasEditPermissions
        {
            get
            {
                return false;
            }
        }

        public override bool HasDeletePermissions
        {
            get
            {
                return false;
            }
        }
    }
}
