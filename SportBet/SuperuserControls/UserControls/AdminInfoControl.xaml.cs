using System.Windows.Controls;
using SportBet.SuperuserControls.ViewModels;

namespace SportBet.SuperuserControls.UserControls
{
    /// <summary>
    /// Interaction logic for AdminInfoControl.xaml
    /// </summary>
    public partial class AdminInfoControl : UserControl
    {
        public AdminInfoControl(AdminInfoViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
