using System;
using SportBet.Models.Edit;

namespace SportBet.EventHandlers.Edit
{
    public delegate void BetEditEventHandler(object sender, BetEditEventArgs e);

    public class BetEditEventArgs : EventArgs
    {
        public BetEditModel Bet { get; private set; }

        public BetEditEventArgs(BetEditModel bet)
        {
            this.Bet = bet;
        }
    }
}
