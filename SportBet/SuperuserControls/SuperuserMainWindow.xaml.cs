using System;
using System.Windows;
using SportBet.Controllers;
using SportBet.Facades;
using SportBet.Services.Contracts;
using SportBet.ControllerFactories;
using SportBet.Contracts.Controllers;
using SportBet.Contracts;

namespace SportBet.SuperuserControls
{
    /// <summary>
    /// Interaction logic for SuperuserMainWindow.xaml
    /// </summary>
    public partial class SuperuserMainWindow : LogWindowBase
    {
        private readonly IAccountController accountController;
        private readonly IClientController clientController;
        private readonly IBookmakerController bookmakerController;
        private readonly IAdminController adminController;
        private readonly IAnalyticController analyticController;

        public SuperuserMainWindow(ControllerFactory controllerFactory, ILogger logger)
            : base(logger)
        {
            InitializeComponent();

            accountController = controllerFactory.CreateAccountController();
            clientController = controllerFactory.CreateClientController();
            bookmakerController = controllerFactory.CreateBookmakerController();
            adminController = controllerFactory.CreateAdminController();
            analyticController = controllerFactory.CreateAnalyticController();

            accountController.ReceivedMessage += (s, e) => SetFooterMessage(e.Success, e.Message);
            clientController.ReceivedMessage += (s, e) => SetFooterMessage(e.Success, e.Message);
            bookmakerController.ReceivedMessage += (s, e) => SetFooterMessage(e.Success, e.Message);
            adminController.ReceivedMessage += (s, e) => SetFooterMessage(e.Success, e.Message);
            analyticController.ReceivedMessage += (s, e) => SetFooterMessage(e.Success, e.Message);

            SetFooterMessage(true, "Welcome, superuser");
        }

        private void RegisterAdmin_Click(object sender, RoutedEventArgs e)
        {
            adminController.Register();
        }

        private void RegisterAnalytic_Click(object sender, RoutedEventArgs e)
        {
            analyticController.Register();
        }

        private void RegisterBookmaker_Click(object sender, RoutedEventArgs e)
        {
            bookmakerController.Register();
        }

        private void RegisterClient_Click(object sender, RoutedEventArgs e)
        {
            clientController.Register();
        }

        private void ManageAdmins_Click(object sender, RoutedEventArgs e)
        {
            adminController.Display();
        }

        private void ManageAnalytics_Click(object sender, RoutedEventArgs e)
        {
            analyticController.Display();
        }

        private void ManageBookmakers_Click(object sender, RoutedEventArgs e)
        {
            bookmakerController.Display();
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
