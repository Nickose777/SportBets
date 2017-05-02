using SportBet.Contracts;
using SportBet.Models.Display;

namespace SportBet.CommonControls.Clients.ViewModels
{
    public class SuperuserManageClientsViewModel : ManageClientsViewModel
    {
        public SuperuserManageClientsViewModel(ISubject subject, FacadeBase<ClientDisplayModel> facade)
            : base(subject, facade) { }
        
        public override bool HasDeletePermissions
        {
            get { return true; }
        }
    }
}
