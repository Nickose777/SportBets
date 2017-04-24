using System.Windows.Controls;
using SportBet.CommonControls.Participants.ViewModels;

namespace SportBet.CommonControls.Participants.UserControls
{
    /// <summary>
    /// Interaction logic for ParticipantCreateControl.xaml
    /// </summary>
    public partial class ParticipantCreateControl : UserControl
    {
        public ParticipantCreateControl(ParticipantCreateViewModel viewModel)
        {
            InitializeComponent(); 
            this.DataContext = viewModel;
        }
    }
}
