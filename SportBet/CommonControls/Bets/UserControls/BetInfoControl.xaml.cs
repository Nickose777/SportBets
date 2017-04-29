using System.Windows.Controls;
using SportBet.CommonControls.Bets.ViewModels;

namespace SportBet.CommonControls.Bets.UserControls
{
    /// <summary>
    /// Interaction logic for BetInfoControl.xaml
    /// </summary>
    public partial class BetInfoControl : UserControl
    {
        public BetInfoControl(BetInfoViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
