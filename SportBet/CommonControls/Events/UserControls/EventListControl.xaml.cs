using System.Windows.Controls;
using SportBet.CommonControls.Events.ViewModels;

namespace SportBet.CommonControls.Events.UserControls
{
    /// <summary>
    /// Interaction logic for EventListControl.xaml
    /// </summary>
    public partial class EventListControl : UserControl
    {
        public EventListControl(EventListViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
