using System;
using System.Windows;
using SportBet.Controllers;
using SportBet.Facades;
using SportBet.Services.Contracts;
using SportBet.ControllerFactories;
using SportBet.Contracts.Controllers;
using SportBet.Contracts;
using System.Windows.Controls;

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

        private UIElement lastElement;

        public SuperuserMainWindow(ControllerFactory controllerFactory, ILogger logger)
            : base(logger)
        {
            InitializeComponent();

            accountController = controllerFactory.CreateAccountController();
            clientController = controllerFactory.CreateClientController();
            bookmakerController = controllerFactory.CreateBookmakerController();
            adminController = controllerFactory.CreateAdminController();
            analyticController = controllerFactory.CreateAnalyticController();

            accountController.ReceivedMessage += (s, e) => UpdateLogs(e.Success, e.Message);
            clientController.ReceivedMessage += (s, e) => UpdateLogs(e.Success, e.Message);
            bookmakerController.ReceivedMessage += (s, e) => UpdateLogs(e.Success, e.Message);
            adminController.ReceivedMessage += (s, e) => UpdateLogs(e.Success, e.Message);
            analyticController.ReceivedMessage += (s, e) => UpdateLogs(e.Success, e.Message);

            UpdateLogs(true, "Welcome, superuser");
        }

        private void RegisterAdmin_Click(object sender, RoutedEventArgs e)
        {
            UIElement element = adminController.GetRegisterElement();
            DisplayElement(element);
        }

        private void RegisterAnalytic_Click(object sender, RoutedEventArgs e)
        {
            UIElement element = analyticController.GetRegisterElement();
            DisplayElement(element);
        }

        private void RegisterBookmaker_Click(object sender, RoutedEventArgs e)
        {
            UIElement element = bookmakerController.GetRegisterElement();
            DisplayElement(element);
        }

        private void RegisterClient_Click(object sender, RoutedEventArgs e)
        {
            UIElement element = clientController.GetRegisterElement();
            DisplayElement(element);
        }

        private void ManageAdmins_Click(object sender, RoutedEventArgs e)
        {
            UIElement element = adminController.GetDisplayElement();
            DisplayElement(element);
        }

        private void ManageAnalytics_Click(object sender, RoutedEventArgs e)
        {
            UIElement element = analyticController.GetDisplayElement();
            DisplayElement(element);
        }

        private void ManageBookmakers_Click(object sender, RoutedEventArgs e)
        {
            UIElement element = bookmakerController.GetDisplayElement();
            DisplayElement(element);
        }

        private void ManageClients_Click(object sender, RoutedEventArgs e)
        {
            UIElement element = clientController.GetDisplayElement();
            DisplayElement(element);
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            UIElement element = accountController.GetPasswordElement();
            DisplayElement(element);
        }

        private void CommonSettings_Click(object sender, RoutedEventArgs e)
        {
            UIElement element = accountController.GetSettingsElement();
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
