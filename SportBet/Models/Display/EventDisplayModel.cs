using System;
using SportBet.Models.Base;

namespace SportBet.Models.Display
{
    public class EventDisplayModel : EventBaseModel
    {
        public DateTime DateOfEvent { get; set; }

        public string Notes { get; set; }
    }
}
