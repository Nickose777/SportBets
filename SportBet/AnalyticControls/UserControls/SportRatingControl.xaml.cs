using System.Windows.Controls;
using SportBet.AnalyticControls.ViewModels;

namespace SportBet.AnalyticControls.UserControls
{
    /// <summary>
    /// Interaction logic for SportRatingControl.xaml
    /// </summary>
    public partial class SportRatingControl : UserControl
    {
        public SportRatingControl(SportRatingViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
