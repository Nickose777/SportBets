using System.Windows;
using System.Windows.Controls;
using SportBet.Contracts;
using SportBet.Contracts.Controllers;
using SportBet.ControllerFactories;

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

        private UIElement lastElement;

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
            UIElement element = clientController.GetRegisterElement();
            DisplayElement(element);
        }

        private void CreateBet_Click(object sender, RoutedEventArgs e)
        {
            UIElement element = betController.GetAddElement();
            DisplayElement(element);
        }

        private void ManageClients_Click(object sender, RoutedEventArgs e)
        {
            UIElement element = clientController.GetDisplayElement();
            DisplayElement(element);
        }

        private void ManageBets_Click(object sender, RoutedEventArgs e)
        {
            UIElement element = betController.GetDisplayElement();
            DisplayElement(element);
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            UIElement element = accountController.GetPasswordElement();
            DisplayElement(element);
        }

        private void ShowLogs_Click(object sender, RoutedEventArgs e)
        {
            ShowLogWindow();
        }

        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            RaiseSignedOutEvent();
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
            if (expander != expander4)
            {
                expander4.IsExpanded = false;
            }
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
    }
}
