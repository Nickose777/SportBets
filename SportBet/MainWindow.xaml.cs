using SportBet.Services.Contracts.Services;
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

        public void Register_Clicked(object sender, EventArgs e)
        {
            Register();
        }

        private void Register()
        {
            IAuthService service = new AuthService();
            AuthResult result = service.Register(new ClientRegisterDTO
            {
                Login = loginTxt.Text,
                Password = passwordTxt.Password,
                ConfirmPassword = passwordConfirmTxt.Password,
                FirstName = firstNameTxt.Text,
                LastName = lastNameTxt.Text,
                PhoneNumber = phoneTxt.Text,
                DateOfBirth = DateTime.Parse(dateOfBirthTxt.Text) //TODO validate date of birth
            });

            if (!result.IsSuccessful)
                MessageBox.Show(result.Message);
            else
                MessageBox.Show("Success!");
        }
    }
}
