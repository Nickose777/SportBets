using System;
using SportBet.Models.Display;

namespace SportBet.EventHandlers.Display
{
    public delegate void ParticipantDisplayEventHandler(object sender, ParticipantDisplayEventArgs e);

    public class ParticipantDisplayEventArgs : EventArgs
    {
        public ParticipantDisplayModel Participant { get; private set; }

        public ParticipantDisplayEventArgs(ParticipantDisplayModel participant)
        {
            this.Participant = participant;
        }
    }
}
