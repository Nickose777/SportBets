using System;
using SportBet.Models.Display;

namespace SportBet.EventHandlers.Display
{
    public delegate void CoefficientDisplayEventHandler(object sender, CoefficientDisplayEventArgs e);

    public class CoefficientDisplayEventArgs : EventArgs
    {
        public CoefficientDisplayModel Coefficient { get; private set; }

        public CoefficientDisplayEventArgs(CoefficientDisplayModel coefficient)
        {
            this.Coefficient = coefficient;
        }
    }
}
