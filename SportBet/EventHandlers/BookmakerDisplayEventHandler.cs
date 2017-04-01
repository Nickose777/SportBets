using SportBet.Models.Display;
using SportBet.Models.Registers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.EventHandlers
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
