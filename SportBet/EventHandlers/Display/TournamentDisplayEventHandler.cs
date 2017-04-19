using System;
using SportBet.Models.Display;

namespace SportBet.EventHandlers.Display
{
    public delegate void TournamentDisplayEventHandler(object sender, TournamentDisplayEventArgs e);

    public class TournamentDisplayEventArgs : EventArgs
    {
        public TournamentDisplayModel Tournament { get; private set; }

        public TournamentDisplayEventArgs(TournamentDisplayModel tournament)
        {
            this.Tournament = tournament;
        }
    }
}
