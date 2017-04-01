using SportBet.Services.Contracts.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SportBet.ClientControls
{
    /// <summary>
    /// Interaction logic for ClientMainWindow.xaml
    /// </summary>
    public partial class ClientMainWindow : Window, ILogoutWindow
    {
        public event EventHandler SignedOut;

        private readonly ServiceFactory factory;

        public ClientMainWindow(ServiceFactory factory, string login)
        {
            InitializeComponent();
            this.factory = factory;

            SetFooterMessage(true, String.Format("Welcome, {0} (client)", login));
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
