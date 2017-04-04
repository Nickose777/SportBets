using SportBet.Models.Edit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
