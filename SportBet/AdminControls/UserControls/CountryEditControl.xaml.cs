using System.Windows.Controls;
using SportBet.AdminControls.ViewModels;

namespace SportBet.AdminControls.UserControls
{
    /// <summary>
    /// Interaction logic for CountryEditControl.xaml
    /// </summary>
    public partial class CountryEditControl : UserControl
    {
        public CountryEditControl(CountryEditViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
