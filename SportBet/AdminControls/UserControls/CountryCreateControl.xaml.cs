using System.Windows.Controls;
using SportBet.AdminControls.ViewModels;

namespace SportBet.AdminControls.UserControls
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
