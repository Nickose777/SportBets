using AutoMapper;
using SportBet.BookmakerControls.UserControls;
using SportBet.BookmakerControls.ViewModels;
using SportBet.Models;
using SportBet.Models.Registers;
using SportBet.Services.Contracts.Factories;
using SportBet.Services.DTOModels;
using SportBet.Services.DTOModels.Register;
using SportBet.Services.ResultTypes;
using SportBet.WindowFactories;
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

namespace SportBet.BookmakerControls
{
    /// <summary>
    /// Interaction logic for BookmakerMainWindow.xaml
    /// </summary>
    public partial class BookmakerMainWindow : Window, ILogoutWindow
    {
        public event EventHandler SignedOut;

        private ServiceFactory factory;

        public BookmakerMainWindow(ServiceFactory factory)
        {
            InitializeComponent();
            this.factory = factory;
        }

        private void RegisterClient_Click(object sender, RoutedEventArgs e)
        {
            RegisterClient();
        }
        
        private void RegisterClient()
        {
            ClientRegisterViewModel viewModel = new ClientRegisterViewModel(new ClientRegisterModel() { DateOfBirth = new DateTime(1990, 01, 01) });
            RegisterClientControl control = new RegisterClientControl(viewModel);

            Window window = WindowFactory.CreateByContentsSize(control);

            viewModel.ClientCreated += (s, e) =>
            {
                ClientRegisterModel client = e.Client;
                ClientRegisterDTO clientDTO = Mapper.Map<ClientRegisterModel, ClientRegisterDTO>(client);

                var service = factory.CreateAccountService();
                ServiceMessage result = service.Register(clientDTO);

                string message;
                if (result.IsSuccessful)
                {
                    message = "Successfully registered new client!";
                    window.Close();
                }
                else
                {
                    message = result.Message;
                }

                MessageBox.Show(message);
            };

            window.ShowDialog();
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
