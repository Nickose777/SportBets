using System;
using SportBet.Models.Create;

namespace SportBet.EventHandlers.Create
{
    public delegate void SportEventHandler(object sender, SportEventArgs e);

    public class SportEventArgs : EventArgs
    {
        public SportCreateModel Sport { get; private set; }

        public SportEventArgs(SportCreateModel sport)
        {
            this.Sport = sport;
        }
    }
}
