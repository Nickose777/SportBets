using System;
using System.Windows;
using AutoMapper;
using SportBet.BookmakerControls.UserControls;
using SportBet.BookmakerControls.ViewModels;
using SportBet.Models.Registers;
using SportBet.Services.Contracts.Factories;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Register;
using SportBet.Services.ResultTypes;
using SportBet.WindowFactories;
using SportBet.Subjects;

namespace SportBet.BookmakerControls
{
    /// <summary>
    /// Interaction logic for BookmakerMainWindow.xaml
    /// </summary>
    public partial class BookmakerMainWindow : MainWindowBase
    {
        public BookmakerMainWindow(ServiceFactory factory, string login)
            : base(factory, login)
        {
            InitializeComponent();

            SetFooterMessage(true, String.Format("Welcome, {0} (bookmaker)", login));
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

                ServiceMessage result;
                using (IAccountService service = factory.CreateAccountService())
                {
                    result = service.Register(clientDTO);
                }

                if (result.IsSuccessful)
                    window.Close();

                SetFooterMessage(result.IsSuccessful, result.Message);
            };

            window.ShowDialog();
        }

        private void ManageClients_Click(object sender, RoutedEventArgs e)
        {
            ManageClients();
        }
        private void ManageClients()
        {
            ClientDisplayManager clientDisplayManager = new ClientDisplayManager(factory);
            clientDisplayManager.ReceivedMessage += (s, e) => SetFooterMessage(e.Success, e.Message);

            clientDisplayManager.DisplayClientsForBookmaker();
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
