using SportBet.Services.Contracts.Services;
using SportBet.Services.Providers;
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
using SportBet.Services.DTOModels;
using SportBet.Services.ResultTypes;
using SportBet.Services.Contracts.Factories;

namespace SportBet
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private void Login()
        {
            using (IAuthService authService = new AuthService())
            {
                UserLoginDTO userLogin = new UserLoginDTO
                {
                    Login = loginTxt.Text,
                    Password = passwordTxt.Password
                };

                AuthServiceFactoryResult result = authService.Login(userLogin);

                if (result.IsSuccessful)
                {
                    loginTxt.Clear();
                    passwordTxt.Clear();

                    LoginType loginType = result.LoginType;
                    ServiceFactory factory = result.Factory;

                    ILogoutWindow logoutWindow = Create(loginType, factory);

                    logoutWindow.SignedOut += (s, e) =>
                    {
                        logoutWindow.Close();
                        this.Show();
                    };
                    logoutWindow.Closed += (s, e) => this.Close();

                    logoutWindow.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Could not login: " + result.Message);
                }
            }
        }

        private ILogoutWindow Create(LoginType loginType, ServiceFactory factory)
        {
            ILogoutWindow window = null;

            switch (loginType)
            {
                case LoginType.Superuser:
                    window = new SuperuserControls.SuperuserMainWindow(factory);
                    break;
                case LoginType.Admin:
                    break;
                case LoginType.Analytic:
                    break;
                case LoginType.Bookmaker:
                    break;
                case LoginType.Client:
                    break;
                case LoginType.NoLogin:
                    break;
                default:
                    break;
            }

            return window;
        }
    }
}
