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
        private readonly IParticipantController participantController;
        private readonly ITournamentController tournamentController;
        private readonly IEventController eventController;
        private readonly ICoefficientController coefficientController;

        public AdminMainWindow(ControllerFactory controllerFactory)
        {
            InitializeComponent();

            accountController = controllerFactory.CreateAccountController();
            countryController = controllerFactory.CreateCountryController();
            sportController = controllerFactory.CreateSportController();
            participantController = controllerFactory.CreateParticipantController();
            tournamentController = controllerFactory.CreateTournamentController();
            eventController = controllerFactory.CreateEventController();
            coefficientController = controllerFactory.CreateCoefficientController();

            accountController.ReceivedMessage += (s, e) => SetFooterMessage(e.Success, e.Message);
            countryController.ReceivedMessage += (s, e) => SetFooterMessage(e.Success, e.Message);
            sportController.ReceivedMessage += (s, e) => SetFooterMessage(e.Success, e.Message);
            participantController.ReceivedMessage += (s, e) => SetFooterMessage(e.Success, e.Message);
            tournamentController.ReceivedMessage += (s, e) => SetFooterMessage(e.Success, e.Message);
            eventController.ReceivedMessage += (s, e) => SetFooterMessage(e.Success, e.Message);
            coefficientController.ReceivedMessage += (s, e) => SetFooterMessage(e.Success, e.Message);

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

        private void CreateParticipant_Click(object sender, RoutedEventArgs e)
        {
            participantController.Create();
        }

        private void CreateTournament_Click(object sender, RoutedEventArgs e)
        {
            tournamentController.Create();
        }

        private void CreateEvent_Click(object sender, RoutedEventArgs e)
        {
            eventController.Create();
        }

        private void CreateCoefficient_Click(object sender, RoutedEventArgs e)
        {
            coefficientController.Create();
        }

        private void ManageCountries_Click(object sender, RoutedEventArgs e)
        {
            countryController.Display();
        }

        private void ManageSports_Click(object sender, RoutedEventArgs e)
        {
            sportController.Display();
        }

        private void ManageParticipants_Click(object sender, RoutedEventArgs e)
        {
            participantController.Display();
        }

        private void ManageTournaments_Click(object sender, RoutedEventArgs e)
        {
            tournamentController.Display();
        }

        private void ManageEvents_Click(object sender, RoutedEventArgs e)
        {
            eventController.Display();
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            accountController.ChangePassword();
        }

        private void SetFooterMessage(bool success, string message)
        {
            //TODO
            //make LogWindow
            string status = success ? "Success!" : "Fail or error!";
            footer.StatusText = status;
            footer.MessageText = message;

            log.Items.Add(status + " " + message);
        }

        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            //TODO
            //MessageBox for question
            RaiseSignedOutEvent();
        }
    }
}
