using System.Windows.Controls;
using SportBet.CommonControls.Sports.ViewModels;

namespace SportBet.CommonControls.Sports.UserControls
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
