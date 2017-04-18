using System.Windows.Controls;
using SportBet.CommonControls.Participants.ViewModels;

namespace SportBet.CommonControls.Participants.UserControls
{
    /// <summary>
    /// Interaction logic for ParticipantListControl.xaml
    /// </summary>
    public partial class ParticipantListControl : UserControl
    {
        public ParticipantListControl(ParticipantListViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
