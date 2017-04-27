using System.Windows.Controls;
using SportBet.CommonControls.Sports.ViewModels;

namespace SportBet.CommonControls.Sports.UserControls
{
    /// <summary>
    /// Interaction logic for SportCreateControl.xaml
    /// </summary>
    public partial class SportCreateControl : UserControl
    {
        public SportCreateControl(SportCreateViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
