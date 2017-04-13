using System.Windows.Controls;
using SportBet.AdminControls.ViewModels;

namespace SportBet.AdminControls.UserControls
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
