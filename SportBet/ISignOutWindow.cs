using System;

namespace SportBet
{
    public interface ISignOutWindow
    {
        event EventHandler SignedOut;
        event EventHandler Closed;

        void Show();
        void Close();
    }
}
