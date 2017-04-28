using System;
using SportBet.Models.Edit;

namespace SportBet.EventHandlers.Edit
{
    public delegate void AdminEditEventHandler(object sender, AdminEditEventArgs e);

    public class AdminEditEventArgs : EventArgs
    {
        public AdminEditModel Admin { get; private set; }

        public AdminEditEventArgs(AdminEditModel admin)
        {
            this.Admin = admin;
        }
    }
}
