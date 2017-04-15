using System.Windows.Controls;
using SportBet.AdminControls.ViewModels;

namespace SportBet.AdminControls.UserControls
{
    /// <summary>
    /// Interaction logic for SportEditControl.xaml
    /// </summary>
    public partial class SportEditControl : UserControl
    {
        public SportEditControl(SportEditViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
