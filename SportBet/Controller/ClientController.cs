using System.Windows;
using AutoMapper;
using SportBet.CommonControls.Clients.UserControls;
using SportBet.CommonControls.Clients.ViewModels;
using SportBet.Contracts;
using SportBet.Contracts.Subjects;
using SportBet.Models.Display;
using SportBet.Models.Edit;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.DTOModels.Edit;
using SportBet.Services.ResultTypes;
using SportBet.WindowFactories;

namespace SportBet.Controllers
{
    class ClientController : SubjectBase, ISubject
    {
        private readonly FacadeBase<ClientDisplayModel> facade;

        public ClientController(ServiceFactory factory, FacadeBase<ClientDisplayModel> facade)
            : base(factory)
        {
            this.facade = facade;
            facade.ReceivedMessage += (s, e) => RaiseReceivedMessageEvent(s, e);
        }

        public void DisplayClientsForAdmin()
        {
            DisplayClients(true);
        }

        public void DisplayClientsForBookmaker()
        {
            DisplayClients(false);
        }

        private void DisplayClients(bool forAdmin)
        {
            ManageClientsViewModel viewModel = new ManageClientsViewModel(this, facade, forAdmin);
            ManageClientsControl control = new ManageClientsControl(viewModel);
            Window window = WindowFactory.CreateByContentsSize(control);

            viewModel.ClientSelectRequest += (s, e) => EditClient(e.Client);

            if (forAdmin)
            {
                viewModel.ClientDeleteRequest += (s, e) =>
                {
                    MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete this user?", "Confirm operation", MessageBoxButton.YesNo);

                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        using (IClientService service = factory.CreateClientService())
                        {
                            ClientDisplayDTO deletedClient = Mapper.Map<ClientDisplayModel, ClientDisplayDTO>(e.Client);
                            ServiceMessage serviceMessage = service.Delete(deletedClient.Login);

                            RaiseReceivedMessageEvent(serviceMessage.IsSuccessful, serviceMessage.Message);
                            if (serviceMessage.IsSuccessful)
                            {
                                Notify();
                            }
                        }
                    }
                };
            }

            window.ShowDialog();
        }

        private void EditClient(ClientDisplayModel clientDisplayModel)
        {
            string login = clientDisplayModel.Login;
            ClientEditModel clientEditModel = new ClientEditModel
            {
                FirstName = clientDisplayModel.FirstName,
                LastName = clientDisplayModel.LastName,
                PhoneNumber = clientDisplayModel.PhoneNumber,
                DateOfBirth = clientDisplayModel.DateOfBirth
            };

            ClientInfoViewModel viewModel = new ClientInfoViewModel(clientEditModel);
            ClientInfoControl control = new ClientInfoControl(viewModel);
            Window window = WindowFactory.CreateByContentsSize(control);

            viewModel.ClientEdited += (s, e) =>
            {
                ClientEditDTO clientEditDTO = Mapper.Map<ClientEditModel, ClientEditDTO>(e.Client);

                using (IClientService service = factory.CreateClientService())
                {
                    ServiceMessage serviceMessage = service.Update(clientEditDTO, login);

                    RaiseReceivedMessageEvent(serviceMessage.IsSuccessful, serviceMessage.Message);
                    if (serviceMessage.IsSuccessful)
                    {
                        window.Close();
                        Notify();
                    }
                }
            };

            window.ShowDialog();
        }
    }
}
