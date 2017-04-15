using System;
using SportBet.Models.Edit;

namespace SportBet.EventHandlers.Edit
{
    public delegate void SportEditEventHandler(object sender, SportEditEventArgs e);

    public class SportEditEventArgs : EventArgs
    {
        public SportEditModel Sport { get; private set; }

        public SportEditEventArgs(SportEditModel sport)
        {
            this.Sport = sport;
        }
    }
}
