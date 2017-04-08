using System;
using System.Windows;
using SportBet.Services.Contracts.Factories;

namespace SportBet.AdminControls
{
    /// <summary>
    /// Interaction logic for AdminMainWindow.xaml
    /// </summary>
    public partial class AdminMainWindow : MainWindowBase
    {
        public AdminMainWindow(ServiceFactory factory, string login)
            : base(factory, login)
        {
            InitializeComponent();

            SetFooterMessage(true, String.Format("Welcome, {0} (admin)", login));
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
