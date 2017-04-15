using System;
using SportBet.Models.Display;

namespace SportBet.EventHandlers.Display
{
    public delegate void BookmakerDisplayEventHandler(object sender, BookmakerDisplayEventArgs e);

    public class BookmakerDisplayEventArgs : EventArgs
    {
        public BookmakerDisplayModel Bookmaker { get; private set; }

        public BookmakerDisplayEventArgs(BookmakerDisplayModel bookmaker)
        {
            this.Bookmaker = bookmaker;
        }
    }
}
