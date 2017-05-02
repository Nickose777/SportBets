using SportBet.Contracts;
using SportBet.Models.Display;

namespace SportBet.CommonControls.Bets.ViewModels
{
    public class BookmakerBetListViewModel : BetListViewModel
    {
        public BookmakerBetListViewModel(ISubject subject, FacadeBase<BetDisplayModel> facade)
            : base(subject, facade) { }

        public override bool HasEditPermissions
        {
            get
            {
                return true;
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
