using SportBet.Services.Contracts.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SportBet
{
    public class MainWindowBase : Window
    {
        public event EventHandler SignedOut;

        protected readonly ServiceFactory factory;
        protected readonly string login;

        public MainWindowBase(ServiceFactory factory, string login)
        {
            this.factory = factory;
            this.login = login;
        }

        protected void RaiseSignedOutEvent()
        {
            var handler = SignedOut;
            if (handler != null)
            {
                EventArgs e = new EventArgs();
                handler(this, e);
            }
        }
    }
}
