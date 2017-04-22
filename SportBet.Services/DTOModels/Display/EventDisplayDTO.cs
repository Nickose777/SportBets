using System;
using SportBet.Services.DTOModels.Base;

namespace SportBet.Services.DTOModels.Display
{
    public class EventDisplayDTO : EventBaseDTO
    {
        public DateTime DateOfEvent { get; set; }

        public string Notes { get; set; }
    }
}
