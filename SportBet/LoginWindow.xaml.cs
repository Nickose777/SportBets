using System;
using System.Windows;
using SportBet.Services.Contracts.Factories;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels;
using SportBet.Services.Providers;
using SportBet.Services.ResultTypes;

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

                    ISignOutWindow logoutWindow = Create(factory, loginType, login);

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

        private ISignOutWindow Create(ServiceFactory factory, LoginType loginType, string login)
        {
            ISignOutWindow window = null;

            switch (loginType)
            {
                case LoginType.Superuser:
                    window = new SuperuserControls.SuperuserMainWindow(factory, login);
                    break;
                case LoginType.Admin:
                    window = new AdminControls.AdminMainWindow(factory, login);
                    break;
                case LoginType.Analytic:
                    window = new AnalyticControls.AnalyticMainWindow(factory, login);
                    break;
                case LoginType.Bookmaker:
                    window = new BookmakerControls.BookmakerMainWindow(factory, login);
                    break;
                case LoginType.Client:
                    window = new ClientControls.ClientMainWindow(factory, login);
                    break;
                case LoginType.NoLogin:
                    MessageBox.Show("Your role wasn't recognized! The program will close");
                    this.Close();
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
