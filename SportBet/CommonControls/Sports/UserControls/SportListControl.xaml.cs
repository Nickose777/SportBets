using System.Windows.Controls;
using SportBet.CommonControls.Sports.ViewModels;

namespace SportBet.CommonControls.Sports.UserControls
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
