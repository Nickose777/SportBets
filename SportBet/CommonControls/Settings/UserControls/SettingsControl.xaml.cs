using System.Windows.Controls;
using SportBet.CommonControls.Settings.ViewModels;

namespace SportBet.CommonControls.Settings.UserControls
{
    /// <summary>
    /// Interaction logic for SettingsUserControl.xaml
    /// </summary>
    public partial class SettingsControl : UserControl
    {
        public SettingsControl(SettingsViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
