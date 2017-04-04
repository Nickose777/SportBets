using SportBet.ClientControls.ViewModels;
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

namespace SportBet.ClientControls.UserControls
{
    /// <summary>
    /// Interaction logic for ClientInfoControl.xaml
    /// </summary>
    public partial class ClientInfoControl : UserControl
    {
        public ClientInfoControl(ClientInfoViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
