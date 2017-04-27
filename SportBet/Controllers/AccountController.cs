using System.Windows;
using SportBet.CommonControls.ChangePassword;
using SportBet.Models;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels;
using SportBet.Services.ResultTypes;
using SportBet.WindowFactories;
using SportBet.Contracts.Controllers;
using System.Windows.Controls;
using SportBet.CommonControls.Clients.ViewModels;
using SportBet.Services.DTOModels.Edit;
using SportBet.Models.Edit;
using AutoMapper;
using SportBet.CommonControls.Clients.UserControls;
using System.Collections.Generic;
using SportBet.CommonControls.Errors;

namespace SportBet.Controllers
{
    class AccountController : ControllerBase, IAccountController
    {
        private readonly string login;

        public AccountController(ServiceFactory factory, string login)
            : base(factory)
        {
            this.login = login;
        }

        public void ChangePassword()
        {
            UIElement element = GetPasswordElement();
            Window window = WindowFactory.CreateByContentsSize(element);

            window.ShowDialog();
        }

        public UIElement GetPasswordElement()
        {
            ChangePasswordControl control = new ChangePasswordControl(login);

            control.PasswordChanged += (s, e) =>
            {
                ChangePasswordModel model = e.ChangePasswordModel;
                if (model.NewPassword == model.ConfirmPassword)
                {
                    ChangePasswordDTO changePasswordDTO = new ChangePasswordDTO
                    {
                        Login = model.Login,
                        OldPassword = model.OldPassword,
                        NewPassword = model.NewPassword
                    };

                    using (IAccountService service = factory.CreateAccountService())
                    {
                        ServiceMessage serviceMessage = service.ChangePassword(changePasswordDTO);
                        RaiseReceivedMessageEvent(serviceMessage.IsSuccessful, serviceMessage.Message);

                        if (serviceMessage.IsSuccessful)
                        {
                            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                            Application.Current.Shutdown();
                        }
                    }
                }
                else
                {
                    RaiseReceivedMessageEvent(false, "Passwords are not same");
                }
            };

            return control;
        }

        public UIElement GetAccountElement()
        {
            DataServiceMessage<ClientEditDTO> serviceMessage;

            using (IClientService service = factory.CreateClientService())
            {
                serviceMessage = service.GetClientInfo(login);
                RaiseReceivedMessageEvent(serviceMessage);
            }

            UIElement element = null;

            if (serviceMessage.IsSuccessful)
            {
                ClientEditModel client = Mapper.Map<ClientEditDTO, ClientEditModel>(serviceMessage.Data);
                ClientInfoViewModel viewModel = new ClientInfoViewModel(client);
                ClientInfoControl control = new ClientInfoControl(viewModel);

                viewModel.ClientEdited += (s, e) => Edit(e.Client);

                element = control;
            }
            else
            {
                List<ServiceMessage> messages = new List<ServiceMessage>()
                {
                    serviceMessage
                };

                ErrorViewModel viewModel = new ErrorViewModel(messages);
                ErrorControl control = new ErrorControl(viewModel);

                element = control;
            }

            return element;
        }

        private void Edit(ClientEditModel clientEditModel)
        {
            ClientEditDTO clientEditDTO = Mapper.Map<ClientEditModel, ClientEditDTO>(clientEditModel);

            using (IClientService service = factory.CreateClientService())
            {
                ServiceMessage serviceMessage = service.Update(clientEditDTO, login);
                RaiseReceivedMessageEvent(serviceMessage);
            }
        }
    }
}
