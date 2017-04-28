using System;
using SportBet.Services.DTOModels.Base;

namespace SportBet.Services.DTOModels.Display
{
    public class BetDisplayDTO : BetBaseDTO
    {
        public decimal Sum { get; set; }

        public DateTime RegistrationDate { get; set; }

        public bool? Win { get; set; }

        public decimal WinSum { get; set; }

        public decimal PossibleWinSum { get; set; }
    }
}
