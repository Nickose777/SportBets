using System;
using SportBet.Models.Base;

namespace SportBet.Models.Create
{
    public class EventCreateModel : EventBaseModel
    {
        public DateTime DateOfEvent { get; set; }

        public string Notes { get; set; }
    }
}
