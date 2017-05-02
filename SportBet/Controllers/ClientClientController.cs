using System;
using SportBet.CommonControls.Clients.ViewModels;
using SportBet.Contracts;
using SportBet.Models.Display;
using SportBet.Models.Edit;
using SportBet.Services.Contracts;

namespace SportBet.Controllers
{
    class ClientClientController : ClientControllerBase
    {
        public ClientClientController(ServiceFactory factory, FacadeBase<ClientDisplayModel> facade)
            : base(factory, facade) { }

        protected override ManageClientsViewModel GetManageClientsViewModel(ISubject subject, FacadeBase<ClientDisplayModel> facade)
        {
            throw new InvalidOperationException();
        }

        protected override ClientInfoViewModel GetClientInfoViewModel(ClientEditModel clientEditModel)
        {
            return new ClientClientInfoViewModel(clientEditModel);
        }
    }
}
