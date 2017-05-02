using SportBet.CommonControls.Clients.ViewModels;

namespace SportBet.DesignTimeViewModels
{
    public class ManageClientsDTViewModel : ManageClientsViewModel
    {
        public ManageClientsDTViewModel()
            : base(null, null) { }

        public override bool HasDeletePermissions
        {
            get { return true; }
        }
    }
}
