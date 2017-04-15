using System;
using SportBet.Models.Display;

namespace SportBet.EventHandlers.Display
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
