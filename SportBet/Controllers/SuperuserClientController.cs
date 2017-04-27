﻿using System;
using System.Windows;
using AutoMapper;
using SportBet.CommomControls.Clients.UserControls;
using SportBet.CommonControls.Clients.UserControls;
using SportBet.CommonControls.Clients.ViewModels;
using SportBet.Contracts;
using SportBet.Contracts.Subjects;
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

namespace SportBet.Controllers
{
    class SuperuserClientController : SubjectBase, ISubject, IClientController
    {
        private readonly FacadeBase<ClientDisplayModel> facade;

        public SuperuserClientController(ServiceFactory factory, FacadeBase<ClientDisplayModel> facade)
            : base(factory)
        {
            this.facade = facade;
            facade.ReceivedMessage += RaiseReceivedMessageEvent;
        }

        public void Register()
        {
            ClientRegisterViewModel viewModel = new ClientRegisterViewModel(new ClientRegisterModel() { DateOfBirth = new DateTime(1990, 01, 01) });
            RegisterClientControl control = new RegisterClientControl(viewModel);
            Window window = WindowFactory.CreateByContentsSize(control);

            viewModel.ClientCreated += (s, e) =>
            {
                ClientRegisterModel client = e.Client;
                ClientRegisterDTO clientDTO = Mapper.Map<ClientRegisterModel, ClientRegisterDTO>(client);

                using (IAccountService service = factory.CreateAccountService())
                {
                    ServiceMessage serviceMessage = service.Register(clientDTO);
                    RaiseReceivedMessageEvent(serviceMessage.IsSuccessful, serviceMessage.Message);

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

            window.Show();
        }

        public void Display()
        {
            ManageClientsViewModel viewModel = new ManageClientsViewModel(this, facade, true);
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
                        if (serviceMessage.IsSuccessful)
                        {
                            Notify();
                        }
                    }
                }
            };

            window.Show();
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

            window.Show();
        }
    }
}
