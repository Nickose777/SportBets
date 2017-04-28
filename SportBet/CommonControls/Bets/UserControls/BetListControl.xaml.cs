using System.Windows.Controls;
using SportBet.CommonControls.Bets.ViewModels;

namespace SportBet.CommonControls.Bets.UserControls
{
    /// <summary>
    /// Interaction logic for BetListControl.xaml
    /// </summary>
    public partial class BetListControl : UserControl
    {
        public BetListControl(BetListViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
