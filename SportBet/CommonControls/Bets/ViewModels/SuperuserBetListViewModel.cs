using SportBet.Contracts;
using SportBet.Models.Display;

namespace SportBet.CommonControls.Bets.ViewModels
{
    public class SuperuserBetListViewModel : BetListViewModel
    {
        public SuperuserBetListViewModel(ISubject subject, FacadeBase<BetDisplayModel> facade)
            : base(subject, facade) { }

        public override bool HasEditPermissions
        {
            get { return true; }
        }

        public override bool HasDeletePermissions
        {
            get { return true; }
        }
    }
}
