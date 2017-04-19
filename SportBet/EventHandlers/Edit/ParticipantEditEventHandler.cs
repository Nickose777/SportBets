using System;
using SportBet.Models.Edit;

namespace SportBet.EventHandlers.Edit
{
    public delegate void ParticipantEditEventHandler(object sender, ParticipantEditEventArgs e);

    public class ParticipantEditEventArgs : EventArgs
    {
        public ParticipantEditModel Participant { get; private set; }

        public ParticipantEditEventArgs(ParticipantEditModel participant)
        {
            this.Participant = participant;
        }
    }
}
