using System;
using SportBet.Models.Create;

namespace SportBet.EventHandlers.Create
{
    public delegate void TournamentEventHandler(object sender, TournamentEventArgs e);

    public class TournamentEventArgs : EventArgs
    {
        public TournamentCreateModel Tournament { get; private set; }

        public TournamentEventArgs(TournamentCreateModel tournament)
        {
            this.Tournament = tournament;
        }
    }
}
