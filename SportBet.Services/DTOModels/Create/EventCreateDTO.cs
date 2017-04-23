using System;
using SportBet.Services.DTOModels.Base;

namespace SportBet.Services.DTOModels.Create
{
    public class EventCreateDTO : EventBaseDTO
    {
        public string Notes { get; set; }
    }
}
