using System.Windows.Controls;
using SportBet.CommonControls.Coefficients.ViewModels;

namespace SportBet.CommonControls.Coefficients.UserControls
{
    /// <summary>
    /// Interaction logic for CoefficientListControl.xaml
    /// </summary>
    public partial class CoefficientListControl : UserControl
    {
        public CoefficientListControl(CoefficientListViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
