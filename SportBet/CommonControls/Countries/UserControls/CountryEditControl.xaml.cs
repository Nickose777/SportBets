using System.Windows.Controls;
using SportBet.CommonControls.Countries.ViewModels;

namespace SportBet.CommonControls.Countries.UserControls
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
