using System;
using System.Windows;

namespace SportBet
{
    public class SignOutWindowBase : Window
    {
        public event EventHandler SignedOut;

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
