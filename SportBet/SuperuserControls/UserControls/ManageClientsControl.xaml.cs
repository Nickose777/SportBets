using SportBet.SuperuserControls.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SportBet.SuperuserControls.UserControls
{
    /// <summary>
    /// Interaction logic for ManageClientsControl.xaml
    /// </summary>
    public partial class ManageClientsControl : UserControl
    {
        public ManageClientsControl(ManageClientsViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
