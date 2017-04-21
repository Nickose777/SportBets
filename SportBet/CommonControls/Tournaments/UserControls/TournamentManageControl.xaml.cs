using System.Windows.Controls;
using SportBet.CommonControls.Tournaments.ViewModels;

namespace SportBet.CommonControls.Tournaments.UserControls
{
    /// <summary>
    /// Interaction logic for TournamentInfoControl.xaml
    /// </summary>
    public partial class TournamentManageControl : UserControl
    {
        public TournamentManageControl(TournamentManageViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
