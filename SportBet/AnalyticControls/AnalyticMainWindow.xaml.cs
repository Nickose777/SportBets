using System;
using System.Windows;
using SportBet.Services.Contracts;
using SportBet.Controllers;

namespace SportBet.AnalyticControls
{
    /// <summary>
    /// Interaction logic for AnalyticMainWindow.xaml
    /// </summary>
    public partial class AnalyticMainWindow : MainWindowBase
    {
        private readonly AccountController accountController;

        public AnalyticMainWindow(ServiceFactory factory, string login)
            : base(factory, login)
        {
            InitializeComponent();

            accountController = new AccountController(factory);

            accountController.ReceivedMessage += (s, e) => SetFooterMessage(e.Success, e.Message);

            SetFooterMessage(true, String.Format("Welcome, {0} (analytic)", login));
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            accountController.ChangePassword(login);
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
