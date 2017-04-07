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
