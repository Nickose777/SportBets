using System;
using SportBet.Models.Base;

namespace SportBet.EventHandlers.Create
{
    public delegate void ParticipantEventHandler(object sender, ParticipantEventArgs e);

    public class ParticipantEventArgs : EventArgs
    {
        public ParticipantBaseModel Participant { get; private set; }

        public ParticipantEventArgs(ParticipantBaseModel participant)
        {
            this.Participant = participant;
        }
    }
}
