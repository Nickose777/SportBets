using SportBet.Models.Display;
using SportBet.Models.Registers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.EventHandlers
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
