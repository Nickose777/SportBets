using System.Windows.Controls;
using SportBet.CommonControls.Clients.ViewModels;

namespace SportBet.CommomControls.Clients.UserControls
{
    /// <summary>
    /// Interaction logic for RegisterClientControl.xaml
    /// </summary>
    public partial class RegisterClientControl : UserControl
    {
        public RegisterClientControl(ClientRegisterViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
