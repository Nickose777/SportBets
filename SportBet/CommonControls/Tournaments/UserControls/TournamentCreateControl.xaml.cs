using System.Windows.Controls;
using SportBet.CommonControls.Tournaments.ViewModels;

namespace SportBet.CommonControls.Tournaments.UserControls
{
    /// <summary>
    /// Interaction logic for TournamentCreateControl.xaml
    /// </summary>
    public partial class TournamentCreateControl : UserControl
    {
        public TournamentCreateControl(TournamentCreateViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
