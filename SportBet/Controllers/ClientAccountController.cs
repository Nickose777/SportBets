using SportBet.CommonControls.Clients.ViewModels;
using SportBet.Models.Edit;
using SportBet.Services.Contracts;

namespace SportBet.Controllers
{
    class ClientAccountController : AccountControllerBase
    {
        public ClientAccountController(ServiceFactory factory, string login)
            : base(factory, login) { }

        protected override ClientInfoViewModel GetClientInfoViewModel(ClientEditModel client)
        {
            return new ClientClientInfoViewModel(client);
        }
    }
}
