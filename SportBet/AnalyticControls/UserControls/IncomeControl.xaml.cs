using System.Windows.Controls;
using SportBet.AnalyticControls.ViewModels;

namespace SportBet.AnalyticControls.UserControls
{
    /// <summary>
    /// Interaction logic for IncomeControl.xaml
    /// </summary>
    public partial class IncomeControl : UserControl
    {
        public IncomeControl(IncomeViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
