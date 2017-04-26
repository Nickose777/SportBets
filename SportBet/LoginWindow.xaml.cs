using System;
using System.Windows;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels;
using SportBet.Services.Providers;
using SportBet.Services.ResultTypes;
using SportBet.ControllerFactories;

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

            loginTxt.Text = "Admin10";
            passwordTxt.Password = "Admin10";

            Login();
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

                LoginServiceMessage result = authService.Login(userLogin);

                if (result.IsSuccessful)
                {
                    loginTxt.Clear();
                    passwordTxt.Clear();

                    LoginType loginType = result.LoginType;
                    ServiceFactory factory = result.Factory;

                    ControllerFactory controllerFactory = new ControllerFactory(factory, loginType, login);

                    SignOutWindowBase window = Create(controllerFactory, loginType);

                    window.SignedOut += (s, e) =>
                    {
                        this.Show();
                        shouldBeClosed = false;
                        window.Close();
                    };
                    window.Closed += (s, e) => this.Close();

                    window.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Could not login: " + result.Message);
                }
            }
        }

        private SignOutWindowBase Create(ControllerFactory controllerFactory, LoginType loginType)
        {
            SignOutWindowBase window = null;

            switch (loginType)
            {
                case LoginType.Superuser:
                    window = new SuperuserControls.SuperuserMainWindow(controllerFactory);
                    break;
                case LoginType.Admin:
                    window = new AdminControls.AdminMainWindow(controllerFactory);
                    break;
                case LoginType.Analytic:
                    window = new AnalyticControls.AnalyticMainWindow(controllerFactory);
                    break;
                case LoginType.Bookmaker:
                    window = new BookmakerControls.BookmakerMainWindow(controllerFactory);
                    break;
                case LoginType.Client:
                    window = new ClientControls.ClientMainWindow(controllerFactory);
                    break;
                case LoginType.NoLogin:
                    MessageBox.Show("Your role wasn't recognized! The program will be closed");
                    this.Close();
                    break;
                default:
                    break;
            }

            window.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

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
