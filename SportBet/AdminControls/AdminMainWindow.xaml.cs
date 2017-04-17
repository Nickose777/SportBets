using System;
using System.Windows;
using SportBet.Services.Contracts;
using SportBet.AdminControls.ViewModels;
using SportBet.AdminControls.UserControls;
using SportBet.WindowFactories;
using SportBet.Services.Contracts.Services;
using SportBet.Services.ResultTypes;
using SportBet.Controllers;
using SportBet.Facades;
using SportBet.Contracts.Controllers;
using SportBet.ControllerFactories;

namespace SportBet.AdminControls
{
    /// <summary>
    /// Interaction logic for AdminMainWindow.xaml
    /// </summary>
    public partial class AdminMainWindow : SignOutWindowBase
    {
        private readonly IAccountController accountController;
        private readonly ICountryController countryController;
        private readonly ISportController sportController;

        public AdminMainWindow(ControllerFactory controllerFactory)
        {
            InitializeComponent();

            accountController = controllerFactory.CreateAccountController();
            countryController = controllerFactory.CreateCountryController();
            sportController = controllerFactory.CreateSportController();

            accountController.ReceivedMessage += (s, e) => SetFooterMessage(e.Success, e.Message);
            countryController.ReceivedMessage += (s, e) => SetFooterMessage(e.Success, e.Message);
            sportController.ReceivedMessage += (s, e) => SetFooterMessage(e.Success, e.Message);

            SetFooterMessage(true, "Welcome, admin");
        }

        private void CreateCountry_Click(object sender, RoutedEventArgs e)
        {
            countryController.Add();
        }

        private void CreateSport_Click(object sender, RoutedEventArgs e)
        {
            sportController.Add();
        }

        private void ManageCountries_Click(object sender, RoutedEventArgs e)
        {
            countryController.Display();
        }

        private void ManageSports_Click(object sender, RoutedEventArgs e)
        {
            sportController.Display();
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
