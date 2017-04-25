using System;
using SportBet.Models.Create;

namespace SportBet.EventHandlers.Create
{
    public delegate void BetCreateEventHandler(object sender, BetCreateEventArgs e);

    public class BetCreateEventArgs : EventArgs
    {
        public BetCreateModel Bet { get; private set; }

        public BetCreateEventArgs(BetCreateModel bet)
        {
            this.Bet = bet;
        }
    }
}
