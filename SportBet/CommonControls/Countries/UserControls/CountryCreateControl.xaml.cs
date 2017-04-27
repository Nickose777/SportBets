using System.Windows.Controls;
using SportBet.CommonControls.Countries.ViewModels;

namespace SportBet.CommonControls.Countries.UserControls
{
    /// <summary>
    /// Interaction logic for CountryCreateControl.xaml
    /// </summary>
    public partial class CountryCreateControl : UserControl
    {
        public CountryCreateControl(CountryCreateViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
