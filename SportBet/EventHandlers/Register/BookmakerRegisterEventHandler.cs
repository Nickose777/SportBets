using System;
using SportBet.Models.Registers;

namespace SportBet.EventHandlers.Register
{
    public delegate void BookmakerRegisterEventHandler(object sender, BookmakerEventArgs e);
    public class BookmakerEventArgs : EventArgs
    {
        public BookmakerRegisterModel Bookmaker { get; private set; }

        public BookmakerEventArgs(BookmakerRegisterModel bookmaker)
        {
            this.Bookmaker = bookmaker;
        }
    }
}
