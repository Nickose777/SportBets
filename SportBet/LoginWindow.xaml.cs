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
            IAuthService authService = new AuthService();

            AuthServiceFactoryResult result = authService.Login(new UserLoginDTO
            {
                Login = loginTxt.Text,
                Password = passwordTxt.Password
            });

            if (result.IsSuccessful)
            {
                MessageBox.Show("Success!");
            }
            else
            {
                MessageBox.Show(result.Message);
            }
        }
    }
}
