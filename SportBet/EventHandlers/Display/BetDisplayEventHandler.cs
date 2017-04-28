using System;
using SportBet.Models.Display;

namespace SportBet.EventHandlers.Display
{
    public delegate void BetDisplayEventHandler(object sender, BetDisplayEventArgs e);

    public class BetDisplayEventArgs : EventArgs
    {
        public BetDisplayModel Bet { get; private set; }

        public BetDisplayEventArgs(BetDisplayModel bet)
        {
            this.Bet = bet;
        }
    }
}
