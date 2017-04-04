using SportBet.Models.Registers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.EventHandlers.Register
{
    public delegate void AdminRegisterEventHandler(object sender, AdminEventArgs e);
    public class AdminEventArgs : EventArgs
    {
        public AdminRegisterModel Admin { get; private set; }

        public AdminEventArgs(AdminRegisterModel admin)
        {
            this.Admin = admin;
        }
    }
}
