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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IAuthService authService = new AuthService();

            AuthServiceFactoryResult result = authService.Login(new UserLoginDTO 
            {
                Login = "Nickose777",
                Password = "Nick2397"
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            IAuthService authService = new AuthService();
            var user = new ClientRegisterDTO
            {
                Login = "Nickose777",
                Password = "Nick2397",
                ConfirmPassword = "Nick2397",
                FirstName = "1",
                LastName = "2",
                PhoneNumber = "3",
                DateOfBirth = new DateTime(1997, 07, 07)
            };
            AuthResult result = authService.Register(user);

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
