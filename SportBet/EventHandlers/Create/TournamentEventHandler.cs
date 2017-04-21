using System;
using SportBet.Models.Create;
using SportBet.Models.Base;

namespace SportBet.EventHandlers.Create
{
    public delegate void TournamentEventHandler(object sender, TournamentEventArgs e);

    public class TournamentEventArgs : EventArgs
    {
        public TournamentBaseModel Tournament { get; private set; }

        public TournamentEventArgs(TournamentBaseModel tournament)
        {
            this.Tournament = tournament;
        }
    }
}
