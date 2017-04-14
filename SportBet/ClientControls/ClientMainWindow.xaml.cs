using System;
using System.Windows;
using System.Windows.Controls;
using AutoMapper;
using SportBet.Models.Edit;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Edit;
using SportBet.Services.ResultTypes;
using SportBet.CommonControls.Clients.ViewModels;
using SportBet.CommonControls.Clients.UserControls;
using SportBet.CommonControls.ChangePassword;
using SportBet.WindowFactories;
using SportBet.Models;
using SportBet.Services.DTOModels;

namespace SportBet.ClientControls
{
    /// <summary>
    /// Interaction logic for ClientMainWindow.xaml
    /// </summary>
    public partial class ClientMainWindow : MainWindowBase
    {
        public ClientMainWindow(ServiceFactory factory, string login)
            : base(factory, login)
        {
            InitializeComponent();

            SetFooterMessage(true, String.Format("Welcome, {0} (client)", login));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataServiceMessage<ClientEditDTO> serviceMessage;
            using (IClientService service = factory.CreateClientService())
            {
                serviceMessage = service.GetClientInfo(login);
            }

            if (serviceMessage.IsSuccessful)
            {
                ClientEditDTO clientDTO = serviceMessage.Data;
                ClientEditModel clientModel = Mapper.Map<ClientEditDTO, ClientEditModel>(clientDTO);

                ClientInfoViewModel viewModel = new ClientInfoViewModel(clientModel);
                ClientInfoControl control = new ClientInfoControl(viewModel);

                viewModel.ClientEdited += (s, ea) =>
                {
                    ServiceMessage response;
                    clientDTO = Mapper.Map<ClientEditModel, ClientEditDTO>(ea.Client);
                    using (IClientService service = factory.CreateClientService())
                    {
                        response = service.Update(clientDTO, login);
                    }

                    SetFooterMessage(response.IsSuccessful, response.Message);
                };

                Grid.SetRow(control, 1);
                mainGrid.Children.Add(control);
            }
            else
            {
                SetFooterMessage(serviceMessage.IsSuccessful, serviceMessage.Message);
            }
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            ChangePassword();
        }
        private void ChangePassword()
        {
            ChangePasswordControl control = new ChangePasswordControl(login);
            Window window = WindowFactory.CreateByContentsSize(control);

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
                        SetFooterMessage(serviceMessage.IsSuccessful, serviceMessage.Message);

                        if (serviceMessage.IsSuccessful)
                        {
                            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                            Application.Current.Shutdown();
                        }
                    }
                }
                else
                {
                    SetFooterMessage(false, "Passwords are not same");
                }
            };

            window.ShowDialog();
        }

        private void SetFooterMessage(bool success, string message)
        {
            footer.StatusText = success ? "Success!" : "Fail or error!";
            footer.MessageText = message;
        }

        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            //TODO
            //MessageBox for question
            RaiseSignedOutEvent();
        }
    }
}
