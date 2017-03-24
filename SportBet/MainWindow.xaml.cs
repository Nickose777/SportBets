using SportBet.Services.Contracts;
using SportBet.Services.DTOModels;
using SportBet.Services.Providers;
using SportBet.Services.ResultTypes;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SportBet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IAuthService service = new AuthService();
            AuthResult result = service.Register(new ClientRegisterDTO
            {
                Login = "Nickose777",
                Password = "needforNick2397",
                ConfirmPassword = "needforNick2397",
                FirstName = "Николай",
                LastName = "Яковлев",
                PhoneNumber = "0631153929",
                DateOfBirth = new DateTime(1997, 07, 23)
            });

            if (!result.IsSuccessful)
                MessageBox.Show(result.Message);
            else
                MessageBox.Show("Success!");
        }
    }
}
