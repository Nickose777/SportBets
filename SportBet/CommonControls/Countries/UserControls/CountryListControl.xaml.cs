using System.Windows.Controls;
using SportBet.CommonControls.Countries.ViewModels;

namespace SportBet.CommonControls.Countries.UserControls
{
    /// <summary>
    /// Interaction logic for CountryListControl.xaml
    /// </summary>
    public partial class CountryListControl : UserControl
    {
        public CountryListControl(CountryListViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
