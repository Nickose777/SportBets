using SportBet.CommonControls.Clients.ViewModels;
using SportBet.Models.Edit;
using SportBet.Services.Contracts;

namespace SportBet.Controllers
{
    class BookmakerAccountController : AccountControllerBase
    {
        public BookmakerAccountController(ServiceFactory factory, string login)
            : base(factory, login) { }

        public override ClientInfoViewModel GetClientInfoViewModel(ClientEditModel client)
        {
            return new BookmakerClientInfoViewModel(client);
        }
    }
}
