using System;
using SportBet.Models.Base;

namespace SportBet.Models.Display
{
    public class BetDisplayModel : BetBaseModel
    {
        public decimal Sum { get; set; }

        public DateTime RegistrationDate { get; set; }

        public bool? Win { get; set; }

        public decimal WinSum { get; set; }

        public decimal PossibleWinSum { get; set; }
    }
}
