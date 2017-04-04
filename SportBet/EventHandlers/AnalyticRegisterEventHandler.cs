using SportBet.Models.Registers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.EventHandlers
{
    public delegate void AnalyticRegisterEventHandler(object sender, AnalyticEventArgs e);
    public class AnalyticEventArgs : EventArgs
    {
        public AnalyticRegisterModel Analytic { get; private set; }

        public AnalyticEventArgs(AnalyticRegisterModel analytic)
        {
            this.Analytic = analytic;
        }
    }
}
