using SportBet.Models.Edit;

namespace SportBet.CommonControls.Clients.ViewModels
{
    public class ClientClientInfoViewModel : ClientInfoViewModel
    {
        public ClientClientInfoViewModel(ClientEditModel client)
            : base(client) { }

        public override bool ShowBetHistory
        {
            get { return false; }
        }
    }
}
