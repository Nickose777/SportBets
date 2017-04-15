using System;
using SportBet.Models.Edit;

namespace SportBet.EventHandlers.Edit
{
    public delegate void ClientEditEventHandler(object sender, ClientEditEventArgs e);

    public class ClientEditEventArgs : EventArgs
    {
        public ClientEditModel Client { get; private set; }

        public ClientEditEventArgs(ClientEditModel client)
        {
            this.Client = client;
        }
    }
}
