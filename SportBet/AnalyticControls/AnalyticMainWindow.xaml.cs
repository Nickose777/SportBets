using System;
using System.Windows;
using SportBet.Services.Contracts.Factories;

namespace SportBet.AnalyticControls
{
    /// <summary>
    /// Interaction logic for AnalyticMainWindow.xaml
    /// </summary>
    public partial class AnalyticMainWindow : Window, ISignOutWindow
    {
        public event EventHandler SignedOut;

        private ServiceFactory factory;

        public AnalyticMainWindow(ServiceFactory factory, string login)
        {
            InitializeComponent();
            this.factory = factory;

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

        private void RaiseSignedOutEvent()
        {
            var handler = SignedOut;
            if (handler != null)
            {
                EventArgs e = new EventArgs();
                handler(this, e);
            }
        }
    }
}
