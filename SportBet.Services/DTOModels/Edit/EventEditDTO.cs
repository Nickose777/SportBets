using System;
using System.Collections.Generic;
using SportBet.Services.DTOModels.Base;

namespace SportBet.Services.DTOModels.Edit
{
    public class EventEditDTO : EventBaseDTO
    {
        public string Notes { get; set; }

        public DateTime NewDateOfEvent { get; set; }

        public List<ParticipantBaseDTO> NewParticipants { get; set; }
    }
}
