using System;
using SportBet.Models.Create;

namespace SportBet.EventHandlers.Create
{
    public delegate void CoefficientCreateEventHandler(object sender, CoefficientCreateEventArgs e);

    public class CoefficientCreateEventArgs : EventArgs
    {
        public CoefficientCreateModel Coefficient { get; private set; }

        public CoefficientCreateEventArgs(CoefficientCreateModel coefficient)
        {
            this.Coefficient = coefficient;
        }
    }
}
