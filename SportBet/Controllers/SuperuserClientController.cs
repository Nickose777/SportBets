﻿using SportBet.CommonControls.Clients.ViewModels;
using SportBet.Contracts;
using SportBet.Models.Display;
using SportBet.Models.Edit;
using SportBet.Services.Contracts;

namespace SportBet.Controllers
{
    class SuperuserClientController : ClientControllerBase
    {
        public SuperuserClientController(ServiceFactory factory, FacadeBase<ClientDisplayModel> facade)
            : base(factory, facade) { }

        protected override ManageClientsViewModel GetManageClientsViewModel(ISubject subject, FacadeBase<ClientDisplayModel> facade)
        {
            return new SuperuserManageClientsViewModel(subject, facade);
        }

        protected override ClientInfoViewModel GetClientInfoViewModel(ClientEditModel clientEditModel)
        {
            return new BookmakerClientInfoViewModel(clientEditModel);
        }
    }
}
