using System.Windows.Controls;
using SportBet.SuperuserControls.ViewModels;

namespace SportBet.SuperuserControls.UserControls
{
    /// <summary>
    /// Interaction logic for BookmakerInfoControl.xaml
    /// </summary>
    public partial class BookmakerInfoControl : UserControl
    {
        public BookmakerInfoControl(BookmakerInfoViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
