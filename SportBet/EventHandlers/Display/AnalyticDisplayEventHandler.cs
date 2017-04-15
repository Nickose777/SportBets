using System;
using SportBet.Models.Display;

namespace SportBet.EventHandlers.Display
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
