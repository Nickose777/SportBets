using System.Windows.Controls;
using SportBet.AdminControls.ViewModels;

namespace SportBet.AdminControls.UserControls
{
    /// <summary>
    /// Interaction logic for SportListControl.xaml
    /// </summary>
    public partial class SportListControl : UserControl
    {
        public SportListControl(SportListViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
