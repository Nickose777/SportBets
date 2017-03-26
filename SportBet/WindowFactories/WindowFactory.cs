using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SportBet.WindowFactories
{
    static class WindowFactory
    {
        public static Window CreateByContentsSize(Control control)
        {
            return new Window
            {
                Content = control,
                SizeToContent = System.Windows.SizeToContent.WidthAndHeight
            };
        }
    }
}
