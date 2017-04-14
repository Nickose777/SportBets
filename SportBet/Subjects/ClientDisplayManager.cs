using System.Collections.Generic;
using System.Windows;
using AutoMapper;
using SportBet.CommonControls.Clients.UserControls;
using SportBet.CommonControls.Clients.ViewModels;
using SportBet.Contracts.Controllers;
using SportBet.Contracts.Observers;
using SportBet.Contracts.Subjects;
using SportBet.Controllers;
using SportBet.EventHandlers;
using SportBet.Models.Display;
using SportBet.Models.Edit;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.DTOModels.Edit;
using SportBet.Services.ResultTypes;
using SportBet.WindowFactories;

namespace SportBet.Subjects
{
    class ClientDisplayManager : SubjectBase, ISubject
    {
        //TODO IClientController in ctor
        public ClientDisplayManager(ServiceFactory factory)
            : base(factory) { }

        public void DisplayClientsForAdmin()
        {
            IClientController controller = new ClientController(factory);
            controller.ReceivedMessage += (s, e) => RaiseReceivedMessageEvent(s, e);

            ManageClientsViewModel viewModel = new ManageClientsViewModel(this, controller, true);
            ManageClientsControl control = new ManageClientsControl(viewModel);
            Window window = WindowFactory.CreateByContentsSize(control);

            viewModel.ClientSelectRequest += (s, e) => EditClient(e.Client);
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
                    }

                    Notify();
                }
            };

            window.ShowDialog();
        }

        public void DisplayClientsForBookmaker()
        {
            IClientController controller = new ClientController(factory);
            controller.ReceivedMessage += (s, e) => RaiseReceivedMessageEvent(s, e);

            ManageClientsViewModel viewModel = new ManageClientsViewModel(this, controller, false);
            ManageClientsControl control = new ManageClientsControl(viewModel);
            Window window = WindowFactory.CreateByContentsSize(control);

            viewModel.ClientSelectRequest += (s, e) => EditClient(e.Client);

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
