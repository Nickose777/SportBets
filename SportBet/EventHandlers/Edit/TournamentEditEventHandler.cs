using System;
using SportBet.Models.Edit;

namespace SportBet.EventHandlers.Edit
{
    public delegate void TournamentEditEventHandler(object sender, TournamentEditEventArgs e);

    public class TournamentEditEventArgs : EventArgs
    {
        public TournamentEditModel Tournament { get; private set; }

        public TournamentEditEventArgs(TournamentEditModel tournament)
        {
            this.Tournament = tournament;
        }
    }
}
