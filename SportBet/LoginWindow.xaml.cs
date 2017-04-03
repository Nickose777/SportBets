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
        private bool shouldBeClosed = true;

        public LoginWindow()
        {
            InitializeComponent();
            this.loginTxt.Focus();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            using (IAuthService service = new AuthService())
            {
                bool connectionEstablished = service.EstablishConnection();
                if (!connectionEstablished)
                {
                    MessageBox.Show("Cannot establish connection to database! The program will be closed");
                    this.Close();
                }
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }
        private void Login()
        {
            using (IAuthService authService = new AuthService())
            {
                string login = loginTxt.Text;
                string password = passwordTxt.Password;

                UserLoginDTO userLogin = new UserLoginDTO
                {
                    Login = login,
                    Password = password
                };

                FactoryServiceMessage result = authService.Login(userLogin);

                if (result.IsSuccessful)
                {
                    loginTxt.Clear();
                    passwordTxt.Clear();

                    LoginType loginType = result.LoginType;
                    ServiceFactory factory = result.Factory;

                    ILogoutWindow logoutWindow = Create(factory, loginType, login);

                    logoutWindow.SignedOut += (s, e) =>
                    {
                        this.Show();
                        shouldBeClosed = false;
                        logoutWindow.Close();
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

        private ILogoutWindow Create(ServiceFactory factory, LoginType loginType, string login)
        {
            ILogoutWindow window = null;

            switch (loginType)
            {
                case LoginType.Superuser:
                    window = new SuperuserControls.SuperuserMainWindow(factory, login);
                    break;
                case LoginType.Admin:
                    window = new AdminControls.AdminMainWindow(factory, login);
                    break;
                case LoginType.Analytic:
                    break;
                case LoginType.Bookmaker:
                    window = new BookmakerControls.BookmakerMainWindow(factory, login);
                    break;
                case LoginType.Client:
                    window = new ClientControls.ClientMainWindow(factory, login);
                    break;
                case LoginType.NoLogin:
                    break;
                default:
                    break;
            }

            return window;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !shouldBeClosed;
            shouldBeClosed = true;
            base.OnClosing(e);
        }
    }
}
