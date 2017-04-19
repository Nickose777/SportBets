using System;
using SportBet.Models.Edit;

namespace SportBet.EventHandlers.Edit
{
    public delegate void BookmakerEditEventHandler(object sender, BookmakerEditEventArgs e);

    public class BookmakerEditEventArgs : EventArgs
    {
        public BookmakerEditModel Bookmaker { get; private set; }

        public BookmakerEditEventArgs(BookmakerEditModel bookmaker)
        {
            this.Bookmaker = bookmaker;
        }
    }
}
