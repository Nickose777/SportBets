using SportBet.Contracts;
using SportBet.Models.Display;

namespace SportBet.CommonControls.Clients.ViewModels
{
    public class BookmakerManageClientsViewModel : ManageClientsViewModel
    {
        public BookmakerManageClientsViewModel(ISubject subject, FacadeBase<ClientDisplayModel> facade)
            : base(subject, facade) { }
        
        public override bool HasDeletePermissions
        {
            get { return false; }
        }
    }
}
