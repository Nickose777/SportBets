using System;
using System.Windows;
using System.Windows.Controls;
using AutoMapper;
using SportBet.ClientControls.UserControls;
using SportBet.ClientControls.ViewModels;
using SportBet.Models.Edit;
using SportBet.Services.Contracts.Factories;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Edit;
using SportBet.Services.ResultTypes;

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
