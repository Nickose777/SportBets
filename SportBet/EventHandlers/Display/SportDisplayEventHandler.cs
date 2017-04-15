using System;

namespace SportBet.EventHandlers.Display
{
    public delegate void SportDisplayEventHandler(object sender, SportDisplayEventArgs e);

    public class SportDisplayEventArgs : EventArgs
    {
        public string SportName { get; private set; }

        public SportDisplayEventArgs(string sportName)
        {
            this.SportName = sportName;
        }
    }
}
