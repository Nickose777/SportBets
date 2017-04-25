using System.Windows.Controls;
using SportBet.CommonControls.Bets.ViewModels;

namespace SportBet.CommonControls.Bets.UserControls
{
    /// <summary>
    /// Interaction logic for BetCreateControl.xaml
    /// </summary>
    public partial class BetCreateControl : UserControl
    {
        public BetCreateControl(BetCreateViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
