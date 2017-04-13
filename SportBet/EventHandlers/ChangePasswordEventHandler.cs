using System;
using SportBet.Models;

namespace SportBet.EventHandlers
{
    public delegate void ChangePasswordEventHandler(object sender, ChangePasswordEventArgs e);

    public class ChangePasswordEventArgs : EventArgs
    {
        public ChangePasswordModel ChangePassword { get; private set; }

        public ChangePasswordEventArgs(ChangePasswordModel changePassword)
        {
            this.ChangePassword = changePassword;
        }
    }
}
