using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using AutoMapper;
using SportBet.Models.Display;
using SportBet.Models.Registers;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.DTOModels.Register;
using SportBet.Services.ResultTypes;
using SportBet.Controllers;
using SportBet.SuperuserControls.UserControls;
using SportBet.SuperuserControls.ViewModels;
using SportBet.WindowFactories;
using SportBet.Facades;

namespace SportBet.SuperuserControls
{
    /// <summary>
    /// Interaction logic for SuperuserMainWindow.xaml
    /// </summary>
    public partial class SuperuserMainWindow : MainWindowBase
    {
        private readonly AccountController accountController;
        private readonly ClientController clientController;
        private readonly BookmakerController bookmakerController;
        private readonly AdminController adminController;
        private readonly AnalyticController analyticController;

        public SuperuserMainWindow(ServiceFactory factory, string login)
            : base(factory, login)
        {
            InitializeComponent();

            accountController = new AccountController(factory);
            clientController = new ClientController(factory, new ClientFacade(factory));
            bookmakerController = new BookmakerController(factory, new BookmakerFacade(factory));
            adminController = new AdminController(factory, new AdminFacade(factory));
            analyticController = new AnalyticController(factory, new AnalyticFacade(factory));

            accountController.ReceivedMessage += (s, e) => SetFooterMessage(e.Success, e.Message);
            clientController.ReceivedMessage += (s, e) => SetFooterMessage(e.Success, e.Message);
            bookmakerController.ReceivedMessage += (s, e) => SetFooterMessage(e.Success, e.Message);
            adminController.ReceivedMessage += (s, e) => SetFooterMessage(e.Success, e.Message);
            analyticController.ReceivedMessage += (s, e) => SetFooterMessage(e.Success, e.Message);

            SetFooterMessage(true, String.Format("Welcome, {0} (superuser)", login));
        }

        private void RegisterBookmaker_Click(object sender, RoutedEventArgs e)
        {
            bookmakerController.RegisterBookmaker();
        }

        private void RegisterAdmin_Click(object sender, RoutedEventArgs e)
        {
            adminController.RegisterAdmin();
        }

        private void RegisterAnalytic_Click(object sender, RoutedEventArgs e)
        {
            analyticController.RegisterAnalytic();
        }

        private void ManageAdmins_Click(object sender, RoutedEventArgs e)
        {
            adminController.DisplayAdmins();
        }

        private void ManageAnalytics_Click(object sender, RoutedEventArgs e)
        {
            analyticController.DisplayAnalytics();
        }

        private void ManageBookmakers_Click(object sender, RoutedEventArgs e)
        {
            bookmakerController.DisplayBookmakers();
        }

        private void ManageClients_Click(object sender, RoutedEventArgs e)
        {
            clientController.DisplayClientsForAdmin();
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
