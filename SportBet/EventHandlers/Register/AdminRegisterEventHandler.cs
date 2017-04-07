using System;
using SportBet.Models.Registers;

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
