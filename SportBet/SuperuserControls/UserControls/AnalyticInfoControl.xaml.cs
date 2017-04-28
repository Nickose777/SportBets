using System.Windows.Controls;
using SportBet.SuperuserControls.ViewModels;

namespace SportBet.SuperuserControls.UserControls
{
    /// <summary>
    /// Interaction logic for AnalyticInfoControl.xaml
    /// </summary>
    public partial class AnalyticInfoControl : UserControl
    {
        public AnalyticInfoControl(AnalyticInfoViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
