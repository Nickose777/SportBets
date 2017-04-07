using System;
using SportBet.Models.Registers;

namespace SportBet.EventHandlers.Register
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
