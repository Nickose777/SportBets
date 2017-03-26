using SportBet.Services.Contracts.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SportBet
{
    public interface ILogoutWindow
    {
        event EventHandler SignedOut;
        event EventHandler Closed;

        void Show();
        void Close();
    }
}
