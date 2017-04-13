using System;

namespace SportBet.EventHandlers.Create
{
    public delegate void SportEventHandler(object sender, SportEventArgs e);

    public class SportEventArgs : EventArgs
    {
        public string SportName { get; private set; }

        public SportEventArgs(string sportName)
        {
            this.SportName = sportName;
        }
    }
}
