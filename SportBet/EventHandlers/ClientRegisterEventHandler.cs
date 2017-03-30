using SportBet.Models.Registers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.EventHandlers
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
