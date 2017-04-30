using System;
using System.Windows;
using AutoMapper;
using SportBet.CommomControls.Clients.UserControls;
using SportBet.CommonControls.Clients.ViewModels;
using SportBet.Contracts;
using SportBet.Models.Display;
using SportBet.Models.Edit;
using SportBet.Models.Registers;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.DTOModels.Edit;
using SportBet.Services.DTOModels.Register;
using SportBet.Services.ResultTypes;
using SportBet.WindowFactories;
using SportBet.Contracts.Controllers;
using SportBet.CommonControls.Clients.UserControls;

namespace SportBet.Controllers
{
    class BookmakerClientController : SubjectBase, ISubject, IClientController
    {
        private readonly FacadeBase<ClientDisplayModel> facade;

        public BookmakerClientController(ServiceFactory factory, FacadeBase<ClientDisplayModel> facade)
            : base(factory)
        {
            this.facade = facade;
            facade.ReceivedMessage += RaiseReceivedMessageEvent;
        }

        public void Register()
        {
            UIElement element = GetRegisterElement();
            Window window = WindowFactory.CreateByContentsSize(element);

            window.Show();
        }

        public void Display()
        {
            UIElement element = GetDisplayElement();
            Window window = WindowFactory.CreateByContentsSize(element);

            window.Show();
        }

        public UIElement GetRegisterElement()
        {
            ClientRegisterViewModel viewModel = new ClientRegisterViewModel(new ClientRegisterModel() { DateOfBirth = new DateTime(1990, 01, 01) });
            RegisterClientControl control = new RegisterClientControl(viewModel);

            viewModel.ClientCreated += (s, e) =>
            {
                ClientRegisterModel client = e.Client;
                ClientRegisterDTO clientDTO = Mapper.Map<ClientRegisterModel, ClientRegisterDTO>(client);

                using (IAccountService service = factory.CreateAccountService())
                {
                    ServiceMessage serviceMessage = service.Register(clientDTO);
                    RaiseReceivedMessageEvent(serviceMessage);

                    if (serviceMessage.IsSuccessful)
                    {
                        viewModel.FirstName = String.Empty;
                        viewModel.LastName = String.Empty;
                        viewModel.PhoneNumber = String.Empty;
                        viewModel.Login = String.Empty;
                        viewModel.Password = String.Empty;
                        viewModel.ConfirmPassword = String.Empty;
                        Notify();
                    }
                }
            };

            return control;
        }

        public UIElement GetDisplayElement()
        {
            ManageClientsViewModel viewModel = new ManageClientsViewModel(this, facade, false);
            ManageClientsControl control = new ManageClientsControl(viewModel);

            viewModel.ClientSelectRequest += (s, e) =>
            {
                string login = e.Client.Login;

                using (IClientService service = factory.CreateClientService())
                {
                    DataServiceMessage<ClientEditDTO> serviceMessage = service.GetClientInfo(login);
                    RaiseReceivedMessageEvent(serviceMessage);

                    if (serviceMessage.IsSuccessful)
                    {
                        ClientEditDTO clientEditDTO = serviceMessage.Data;
                        ClientEditModel clientEditModel = Mapper.Map<ClientEditDTO, ClientEditModel>(clientEditDTO);

                        Edit(clientEditModel);
                    }
                }
            };

            return control;
        }

        private void Edit(ClientEditModel clientEditModel)
        {
            ClientInfoViewModel viewModel = new ClientInfoViewModel(clientEditModel);
            ClientInfoControl control = new ClientInfoControl(viewModel);
            Window window = WindowFactory.CreateByContentsSize(control);

            viewModel.ClientEdited += (s, e) =>
            {
                ClientEditDTO clientEditDTO = Mapper.Map<ClientEditModel, ClientEditDTO>(e.Client);

                using (IClientService service = factory.CreateClientService())
                {
                    ServiceMessage serviceMessage = service.Update(clientEditDTO);

                    RaiseReceivedMessageEvent(serviceMessage.IsSuccessful, serviceMessage.Message);
                    if (serviceMessage.IsSuccessful)
                    {
                        window.Close();
                        Notify();
                    }
                }
            };

            window.Show();
        }
    }
}
