using System.Windows.Controls;
using SportBet.CommonControls.Events.ViewModels;

namespace SportBet.CommonControls.Events.UserControls
{
    /// <summary>
    /// Interaction logic for EventManageControl.xaml
    /// </summary>
    public partial class EventManageControl : UserControl
    {
        public EventManageControl(EventManageViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
