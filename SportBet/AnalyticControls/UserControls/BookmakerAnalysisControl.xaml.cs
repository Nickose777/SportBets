using System.Windows.Controls;
using SportBet.AnalyticControls.ViewModels;

namespace SportBet.AnalyticControls.UserControls
{
    /// <summary>
    /// Interaction logic for BookmakerAnalysisControl.xaml
    /// </summary>
    public partial class BookmakerAnalysisControl : UserControl
    {
        public BookmakerAnalysisControl(BookmakerAnalysisViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
