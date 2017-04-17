using System.Windows;
using SportBet.Contracts.Controllers;
using SportBet.ControllerFactories;

namespace SportBet.AnalyticControls
{
    /// <summary>
    /// Interaction logic for AnalyticMainWindow.xaml
    /// </summary>
    public partial class AnalyticMainWindow : SignOutWindowBase
    {
        private readonly IAccountController accountController;

        public AnalyticMainWindow(ControllerFactory controllerFactory)
        {
            InitializeComponent();

            accountController = controllerFactory.CreateAccountController();

            accountController.ReceivedMessage += (s, e) => SetFooterMessage(e.Success, e.Message);

            SetFooterMessage(true, "Welcome, analytic");
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
