using System.Windows.Controls;
using SportBet.CommonControls.Tournaments.ViewModels;

namespace SportBet.CommonControls.Tournaments.UserControls
{
    /// <summary>
    /// Interaction logic for TournamentListControl.xaml
    /// </summary>
    public partial class TournamentListControl : UserControl
    {
        public TournamentListControl(TournamentListViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
