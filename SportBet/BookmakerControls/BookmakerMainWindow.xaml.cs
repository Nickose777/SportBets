using System;
using System.Windows;
using AutoMapper;
using SportBet.Models.Registers;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Register;
using SportBet.Services.ResultTypes;
using SportBet.WindowFactories;
using SportBet.Controllers;
using SportBet.Facades;

namespace SportBet.BookmakerControls
{
    /// <summary>
    /// Interaction logic for BookmakerMainWindow.xaml
    /// </summary>
    public partial class BookmakerMainWindow : MainWindowBase
    {
        private readonly AccountController accountController;
        private readonly ClientController clientController;

        public BookmakerMainWindow(ServiceFactory factory, string login)
            : base(factory, login)
        {
            InitializeComponent();

            accountController = new AccountController(factory);
            clientController = new ClientController(factory, new ClientFacade(factory));

            accountController.ReceivedMessage += (s, e) => SetFooterMessage(e.Success, e.Message);
            clientController.ReceivedMessage += (s, e) => SetFooterMessage(e.Success, e.Message);

            SetFooterMessage(true, String.Format("Welcome, {0} (bookmaker)", login));
        }

        private void RegisterClient_Click(object sender, RoutedEventArgs e)
        {
            RegisterClient();
        }
        private void RegisterClient()
        {
            clientController.RegisterClient();
        }

        private void ManageClients_Click(object sender, RoutedEventArgs e)
        {
            clientController.DisplayClientsForBookmaker();
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            accountController.ChangePassword(login);
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
