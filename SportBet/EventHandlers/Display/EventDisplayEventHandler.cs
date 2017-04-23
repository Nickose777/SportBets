using System;
using SportBet.Models.Display;

namespace SportBet.EventHandlers.Display
{
    public delegate void EventDisplayEventHandler(object sender, EventDisplayEventArgs e);

    public class EventDisplayEventArgs : EventArgs
    {
        public EventDisplayModel Event { get; private set; }

        public EventDisplayEventArgs(EventDisplayModel _event)
        {
            this.Event = _event;
        }
    }
}
