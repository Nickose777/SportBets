using System;
using SportBet.Models.Registers;

namespace SportBet.EventHandlers.Register
{
    public delegate void ClientRegisterEventHandler(object sender, ClientEventArgs e);
    public class ClientEventArgs : EventArgs
    {
        public ClientRegisterModel Client { get; private set; }

        public ClientEventArgs(ClientRegisterModel client)
        {
            this.Client = client;
        }
    }
}
