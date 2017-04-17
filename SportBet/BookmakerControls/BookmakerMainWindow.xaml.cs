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
using SportBet.ControllerFactories;
using SportBet.Contracts.Controllers;

namespace SportBet.BookmakerControls
{
    /// <summary>
    /// Interaction logic for BookmakerMainWindow.xaml
    /// </summary>
    public partial class BookmakerMainWindow : SignOutWindowBase
    {
        private readonly IAccountController accountController;
        private readonly IClientController clientController;

        public BookmakerMainWindow(ControllerFactory controllerFactory)
        {
            InitializeComponent();

            accountController = controllerFactory.CreateAccountController();
            clientController = controllerFactory.CreateClientController();

            accountController.ReceivedMessage += (s, e) => SetFooterMessage(e.Success, e.Message);
            clientController.ReceivedMessage += (s, e) => SetFooterMessage(e.Success, e.Message);

            SetFooterMessage(true, "Welcome, bookmaker");
        }

        private void RegisterClient_Click(object sender, RoutedEventArgs e)
        {
            RegisterClient();
        }
        private void RegisterClient()
        {
            clientController.Register();
        }

        private void ManageClients_Click(object sender, RoutedEventArgs e)
        {
            clientController.Display();
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            accountController.ChangePassword();
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
