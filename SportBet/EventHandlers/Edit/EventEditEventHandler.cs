using System;
using SportBet.Models.Edit;

namespace SportBet.EventHandlers.Edit
{
    public delegate void EventEditEventHandler(object sender, EventEditEventArgs e);

    public class EventEditEventArgs : EventArgs
    {
        public EventEditModel Event { get; private set; }

        public EventEditEventArgs(EventEditModel _event)
        {
            this.Event = _event;
        }
    }
}
