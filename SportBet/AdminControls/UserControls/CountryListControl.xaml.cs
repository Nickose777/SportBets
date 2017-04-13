using System.Windows.Controls;
using SportBet.AdminControls.ViewModels;

namespace SportBet.AdminControls.UserControls
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
