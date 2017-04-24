using System;
using SportBet.Models.Edit;

namespace SportBet.EventHandlers.Edit
{
    public delegate void CoefficientEditEventHandler(object sender, CoefficientEditEventArgs e);

    public class CoefficientEditEventArgs : EventArgs
    {
        public CoefficientEditModel Coefficient { get; private set; }

        public CoefficientEditEventArgs(CoefficientEditModel coefficient)
        {
            this.Coefficient = coefficient;
        }
    }
}
