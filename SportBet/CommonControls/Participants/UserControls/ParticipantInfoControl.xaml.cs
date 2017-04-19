using System.Windows.Controls;
using SportBet.CommonControls.Participants.ViewModels;

namespace SportBet.CommonControls.Participants.UserControls
{
    /// <summary>
    /// Interaction logic for ParticipantInfoControl.xaml
    /// </summary>
    public partial class ParticipantInfoControl : UserControl
    {
        public ParticipantInfoControl(ParticipantInfoViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
