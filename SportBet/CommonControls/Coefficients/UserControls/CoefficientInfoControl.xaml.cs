using System.Windows.Controls;
using SportBet.CommonControls.Coefficients.ViewModels;

namespace SportBet.CommonControls.Coefficients.UserControls
{
    /// <summary>
    /// Interaction logic for CoefficientInfoControl.xaml
    /// </summary>
    public partial class CoefficientInfoControl : UserControl
    {
        public CoefficientInfoControl(CoefficientInfoViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
