using System.Windows.Controls;
using SportBet.AnalyticControls.ViewModels;

namespace SportBet.AnalyticControls.UserControls
{
    /// <summary>
    /// Interaction logic for ClientAnalysisControl.xaml
    /// </summary>
    public partial class ClientAnalysisControl : UserControl
    {
        public ClientAnalysisControl(ClientAnalysisViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
