using System.Windows.Controls;
using SportBet.CommonControls.Tournaments.ViewModels;

namespace SportBet.CommonControls.Tournaments.UserControls
{
    /// <summary>
    /// Interaction logic for TournamentInfoControl.xaml
    /// </summary>
    public partial class TournamentInfoControl : UserControl
    {
        public TournamentInfoControl(TournamentInfoViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
