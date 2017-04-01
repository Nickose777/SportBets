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

namespace SportBet.CommonControls
{
    /// <summary>
    /// Interaction logic for Footer.xaml
    /// </summary>
    public partial class Footer : UserControl
    {
        public string StatusText
        {
            get { return statusText.Text; }
            set { statusText.Text = value; }
        }
        public string MessageText
        {
            get { return messageText.Text; }
            set { messageText.Text = value; }
        }

        public Footer()
        {
            InitializeComponent();
        }
    }
}
