using SportBet.Models.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.EventHandlers
{
    public delegate void AnalyticDisplayEventHandler(object sender, AnalyticDisplayEventArgs e);
    public class AnalyticDisplayEventArgs : EventArgs
    {
        public AnalyticDisplayModel Analytic { get; private set; }

        public AnalyticDisplayEventArgs(AnalyticDisplayModel analytic)
        {
            this.Analytic = analytic;
        }
    }
}
