using SportBet.CommonControls.Bets.ViewModels;

namespace SportBet.DesignTimeViewModels
{
    public class BetListDTViewModel : BetListViewModel
    {
        public BetListDTViewModel()
            : base(null, null) { }

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
