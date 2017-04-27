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
using SportBet.Contracts;

namespace SportBet.BookmakerControls
{
    /// <summary>
    /// Interaction logic for BookmakerMainWindow.xaml
    /// </summary>
    public partial class BookmakerMainWindow : LogWindowBase
    {
        private readonly IAccountController accountController;
        private readonly IClientController clientController;
        private readonly IBetController betController;

        public BookmakerMainWindow(ControllerFactory controllerFactory, ILogger logger)
            : base(logger)
        {
            InitializeComponent();

            accountController = controllerFactory.CreateAccountController();
            clientController = controllerFactory.CreateClientController();
            betController = controllerFactory.CreateBetController();

            accountController.ReceivedMessage += (s, e) => UpdateLogs(e.Success, e.Message);
            clientController.ReceivedMessage += (s, e) => UpdateLogs(e.Success, e.Message);
            betController.ReceivedMessage += (s, e) => UpdateLogs(e.Success, e.Message);

            UpdateLogs(true, "Welcome, bookmaker");
        }

        private void RegisterClient_Click(object sender, RoutedEventArgs e)
        {
            RegisterClient();
        }
        private void RegisterClient()
        {
            clientController.Register();
        }

        private void CreateBet_Click(object sender, RoutedEventArgs e)
        {
            betController.Create();
        }

        private void ManageClients_Click(object sender, RoutedEventArgs e)
        {
            clientController.Display();
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            accountController.ChangePassword();
        }

        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            RaiseSignedOutEvent();
        }
    }
}
