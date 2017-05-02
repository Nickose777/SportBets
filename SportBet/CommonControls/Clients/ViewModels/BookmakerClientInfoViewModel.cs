using SportBet.Models.Edit;
namespace SportBet.CommonControls.Clients.ViewModels
{
    public class BookmakerClientInfoViewModel : ClientInfoViewModel
    {
        public BookmakerClientInfoViewModel(ClientEditModel client)
            : base(client) { }

        public override bool ShowBetHistory
        {
            get { return true; }
        }
    }
}
