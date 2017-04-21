using System;
using SportBet.Models.Create;

namespace SportBet.EventHandlers.Create
{
    public delegate void EventCreateEventHandler(object sender, EventEventArgs e);

    public class EventEventArgs : EventArgs
    {
        public EventCreateModel Event { get; private set; }

        public EventEventArgs(EventCreateModel eventCreateModel)
        {
            this.Event = eventCreateModel;
        }
    }
}
