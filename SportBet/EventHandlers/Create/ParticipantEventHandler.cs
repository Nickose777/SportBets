using System;
using SportBet.Models.Create;

namespace SportBet.EventHandlers.Create
{
    public delegate void ParticipantEventHandler(object sender, ParticipantEventArgs e);

    public class ParticipantEventArgs : EventArgs
    {
        public ParticipantCreateModel Participant { get; private set; }

        public ParticipantEventArgs(ParticipantCreateModel participant)
        {
            this.Participant = participant;
        }
    }
}
