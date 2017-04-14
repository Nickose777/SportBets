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

namespace SportBet.AdminControls
{
    /// <summary>
    /// Interaction logic for AdminMainWindow.xaml
    /// </summary>
    public partial class AdminMainWindow : MainWindowBase
    {
        private readonly CountryController countryController;
        private readonly SportController sportController;

        public AdminMainWindow(ServiceFactory factory, string login)
            : base(factory, login)
        {
            InitializeComponent();

            countryController = new CountryController(factory, new CountryFacade(factory));
            sportController = new SportController(factory, new SportFacade(factory));

            countryController.ReceivedMessage += (s, e) => SetFooterMessage(e.Success, e.Message);
            sportController.ReceivedMessage += (s, e) => SetFooterMessage(e.Success, e.Message);

            SetFooterMessage(true, String.Format("Welcome, {0} (admin)", login));
        }

        private void CreateCountry_Click(object sender, RoutedEventArgs e)
        {
            countryController.AddCountry();
        }

        private void CreateSport_Click(object sender, RoutedEventArgs e)
        {
            sportController.AddSport();
        }

        private void ManageCountries_Click(object sender, RoutedEventArgs e)
        {
            countryController.DisplayCountries();
        }

        private void ManageSports_Click(object sender, RoutedEventArgs e)
        {
            sportController.DisplaySports();
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
