using System.Windows.Controls;
using SportBet.CommonControls.Clients.ViewModels;

namespace SportBet.CommonControls.Clients.UserControls
{
    /// <summary>
    /// Interaction logic for ClientInfoControl.xaml
    /// </summary>
    public partial class ClientInfoControl : UserControl
    {
        public ClientInfoControl(ClientInfoViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
