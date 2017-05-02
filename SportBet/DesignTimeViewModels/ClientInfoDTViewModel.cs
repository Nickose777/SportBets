using SportBet.CommonControls.Clients.ViewModels;

namespace SportBet.DesignTimeViewModels
{
    public class ClientInfoDTViewModel : ClientInfoViewModel
    {
        public ClientInfoDTViewModel()
            : base(null) { }

        public override bool ShowBetHistory
        {
            get { return true; }
        }
    }
}
