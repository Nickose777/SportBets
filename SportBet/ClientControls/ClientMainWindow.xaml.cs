using System.Windows;
using System.Windows.Controls;
using SportBet.Contracts;
using SportBet.Contracts.Controllers;
using SportBet.ControllerFactories;

namespace SportBet.ClientControls
{
    /// <summary>
    /// Interaction logic for ClientMainWindow.xaml
    /// </summary>
    public partial class ClientMainWindow : LogWindowBase
    {
        private readonly IAccountController accountController;

        private UIElement lastElement;

        public ClientMainWindow(ControllerFactory controllerFactory, ILogger logger)
            : base(logger)
        {
            InitializeComponent();

            accountController = controllerFactory.CreateAccountController();

            accountController.ReceivedMessage += (s, e) => UpdateLogs(e.Success, e.Message);

            UpdateLogs(true, "Welcome, client");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UIElement element = accountController.GetAccountElement();
            DisplayElement(element);
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            UIElement element = accountController.GetPasswordElement();
            DisplayElement(element);
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
        }

        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            RaiseSignedOutEvent();
        }

        private void DisplayAccount_Click(object sender, RoutedEventArgs e)
        {
            UIElement element = accountController.GetAccountElement();
            DisplayElement(element);
        }

        private void ShowLogs_Click(object sender, RoutedEventArgs e)
        {
            ShowLogWindow();
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
