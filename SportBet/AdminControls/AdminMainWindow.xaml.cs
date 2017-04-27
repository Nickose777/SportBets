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
using System.Windows.Controls;

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

        private UIElement lastElement;

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
            UIElement element = countryController.GetAddElement();
            DisplayElement(element);
        }

        private void CreateSport_Click(object sender, RoutedEventArgs e)
        {
            UIElement element = sportController.GetAddElement();
            DisplayElement(element);
        }

        private void CreateParticipant_Click(object sender, RoutedEventArgs e)
        {
            UIElement element = participantController.GetAddElement();
            DisplayElement(element);
        }

        private void CreateTournament_Click(object sender, RoutedEventArgs e)
        {
            UIElement element = tournamentController.GetAddElement();
            DisplayElement(element);
        }

        private void CreateEvent_Click(object sender, RoutedEventArgs e)
        {
            UIElement element = eventController.GetAddElement();
            DisplayElement(element);
        }

        private void CreateCoefficient_Click(object sender, RoutedEventArgs e)
        {
            UIElement element = coefficientController.GetAddElement();
            DisplayElement(element);
        }

        private void ManageCountries_Click(object sender, RoutedEventArgs e)
        {
            UIElement element = countryController.GetDisplayElement();
            DisplayElement(element);
        }

        private void ManageSports_Click(object sender, RoutedEventArgs e)
        {
            UIElement element = sportController.GetDisplayElement();
            DisplayElement(element);
        }

        private void ManageParticipants_Click(object sender, RoutedEventArgs e)
        {
            UIElement element = participantController.GetDisplayElement();
            DisplayElement(element);
        }

        private void ManageTournaments_Click(object sender, RoutedEventArgs e)
        {
            UIElement element = tournamentController.GetDisplayElement();
            DisplayElement(element);
        }

        private void ManageEvents_Click(object sender, RoutedEventArgs e)
        {
            UIElement element = eventController.GetDisplayElement();
            DisplayElement(element);
        }

        private void ManageCoefficients_Click(object sender, RoutedEventArgs e)
        {
            UIElement element = coefficientController.GetDisplayElement();
            DisplayElement(element);
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            UIElement element = accountController.GetPasswordElement();
            DisplayElement(element);
        }

        private void SetFooterMessage(bool success, string message)
        {
            //TODO
            //make LogWindow
            string status = success ? "Success!" : "Fail or error!";
            //footer.StatusText = status;
            //footer.MessageText = message;
        }

        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            //TODO
            //MessageBox for question
            RaiseSignedOutEvent();
        }

        private void DisplayElement(UIElement element)
        {
            if (lastElement != null)
            {
                mainGrid.Children.Remove(lastElement);
            }

            Grid.SetRow(element, 0);
            Grid.SetColumn(element, 1);

            mainGrid.Children.Add(element);
            lastElement = element;
        }

        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            Expander expander = sender as Expander;

            if (expander != expander1)
            {
                expander1.IsExpanded = false;
            }
            if (expander != expander2)
            {
                expander2.IsExpanded = false;
            }
            if (expander != expander3)
            {
                expander3.IsExpanded = false;
            }
        }
    }
}
