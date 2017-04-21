using System;
using System.Collections.Generic;
using SportBet.Services.DTOModels.Base;

namespace SportBet.Services.DTOModels.Edit
{
    public class TournamentEditDTO : TournamentBaseDTO
    {
        public string NewName { get; set; }

        public DateTime NewDateOfStart { get; set; }

        public List<ParticipantBaseDTO> Participants { get; set; }
    }
}
