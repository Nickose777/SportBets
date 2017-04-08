using System;
using System.Windows;
using SportBet.Services.Contracts.Factories;

namespace SportBet.AnalyticControls
{
    /// <summary>
    /// Interaction logic for AnalyticMainWindow.xaml
    /// </summary>
    public partial class AnalyticMainWindow : MainWindowBase
    {
        public AnalyticMainWindow(ServiceFactory factory, string login)
            : base(factory, login)
        {
            InitializeComponent();

            SetFooterMessage(true, String.Format("Welcome, {0} (analytic)", login));
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
