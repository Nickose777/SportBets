using System.Windows;
using System.Windows.Controls;
using SportBet.EventHandlers;
using SportBet.Models;

namespace SportBet.CommonControls.ChangePassword
{
    /// <summary>
    /// Interaction logic for ChangePasswordControl.xaml
    /// </summary>
    public partial class ChangePasswordControl : UserControl
    {
        public event ChangePasswordEventHandler PasswordChanged;

        private readonly string login;

        public ChangePasswordControl(string login)
        {
            InitializeComponent();
            this.login = login;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordModel model = new ChangePasswordModel
            {
                Login = login,
                OldPassword = oldTxt.Password,
                NewPassword = newTxt.Password,
                ConfirmPassword = confirmTxt.Password
            };

            RaisePasswordChangedEvent(model);
        }

        private void RaisePasswordChangedEvent(ChangePasswordModel model)
        {
            var handler = PasswordChanged;
            if (handler != null)
            {
                ChangePasswordEventArgs e = new ChangePasswordEventArgs(model);
                handler(this, e);
            }
        }
    }
}
