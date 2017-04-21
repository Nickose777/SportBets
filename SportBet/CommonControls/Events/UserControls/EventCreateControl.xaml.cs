using System.Windows.Controls;
using SportBet.CommonControls.Events.ViewModels;

namespace SportBet.CommonControls.Events.UserControls
{
    /// <summary>
    /// Interaction logic for EventCreateControl.xaml
    /// </summary>
    public partial class EventCreateControl : UserControl
    {
        public EventCreateControl(EventCreateViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
