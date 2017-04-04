using SportBet.Models.Registers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
