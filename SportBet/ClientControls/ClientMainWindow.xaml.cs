using AutoMapper;
using SportBet.ClientControls.UserControls;
using SportBet.ClientControls.ViewModels;
using SportBet.Models.Edit;
using SportBet.Services.Contracts.Factories;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels;
using SportBet.Services.DTOModels.Edit;
using SportBet.Services.ResultTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SportBet.ClientControls
{
    /// <summary>
    /// Interaction logic for ClientMainWindow.xaml
    /// </summary>
    public partial class ClientMainWindow : Window, ILogoutWindow
    {
        public event EventHandler SignedOut;

        private readonly ServiceFactory factory;
        private readonly string login;

        public ClientMainWindow(ServiceFactory factory, string login)
        {
            InitializeComponent();

            this.factory = factory;
            this.login = login;

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
                    MessageBox.Show("TODO - save changes");
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

        private void RaiseSignedOutEvent()
        {
            var handler = SignedOut;
            if (handler != null)
            {
                EventArgs e = new EventArgs();
                handler(this, e);
            }
        }
    }
}
