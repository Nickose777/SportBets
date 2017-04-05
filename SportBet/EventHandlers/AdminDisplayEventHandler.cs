using SportBet.Models.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.EventHandlers
{
    public delegate void AdminDisplayEventHandler(object sender, AdminDisplayEventArgs e);
    public class AdminDisplayEventArgs : EventArgs
    {
        public AdminDisplayModel Admin { get; private set; }

        public AdminDisplayEventArgs(AdminDisplayModel admin)
        {
            this.Admin = admin;
        }
    }
}
