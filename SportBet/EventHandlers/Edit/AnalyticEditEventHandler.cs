using System;
using SportBet.Models.Edit;

namespace SportBet.EventHandlers.Edit
{
    public delegate void AnalyticEditEventHandler(object sender, AnalyticEditEventArgs e);

    public class AnalyticEditEventArgs : EventArgs
    {
        public AnalyticEditModel Analytic { get; private set; }

        public AnalyticEditEventArgs(AnalyticEditModel analytic)
        {
            this.Analytic = analytic;
        }
    }
}
