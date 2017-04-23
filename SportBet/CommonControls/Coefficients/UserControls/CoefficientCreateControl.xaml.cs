using System.Windows.Controls;
using SportBet.CommonControls.Coefficients.ViewModels;

namespace SportBet.CommonControls.Coefficients.UserControls
{
    /// <summary>
    /// Interaction logic for CoefficientCreateControl.xaml
    /// </summary>
    public partial class CoefficientCreateControl : UserControl
    {
        public CoefficientCreateControl(CoefficientCreateViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
