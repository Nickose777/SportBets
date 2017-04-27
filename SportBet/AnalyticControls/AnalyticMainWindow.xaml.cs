using System.Windows;
using SportBet.Contracts.Controllers;
using SportBet.ControllerFactories;
using SportBet.Contracts;

namespace SportBet.AnalyticControls
{
    /// <summary>
    /// Interaction logic for AnalyticMainWindow.xaml
    /// </summary>
    public partial class AnalyticMainWindow : LogWindowBase
    {
        private readonly IAccountController accountController;

        public AnalyticMainWindow(ControllerFactory controllerFactory, ILogger logger)
            : base(logger)
        {
            InitializeComponent();

            accountController = controllerFactory.CreateAccountController();

            accountController.ReceivedMessage += (s, e) => UpdateLogs(e.Success, e.Message);

            UpdateLogs(true, "Welcome, analytic");
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
