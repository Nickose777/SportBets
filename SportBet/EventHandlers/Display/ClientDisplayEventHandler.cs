using System;
using SportBet.Models.Display;

namespace SportBet.EventHandlers.Display
{
    public delegate void ClientDisplayEventHandler(object sender, ClientDisplayEventArgs e);

    public class ClientDisplayEventArgs : EventArgs
    {
        public ClientDisplayModel Client { get; private set; }

        public ClientDisplayEventArgs(ClientDisplayModel client)
        {
            this.Client = client;
        }
    }
}
