using System;
using System.Collections.Generic;

namespace SportBet.Services.DTOModels.Base
{
    public class CoefficientBaseDTO
    {
        public string SportName { get; set; }

        public string TournamentName { get; set; }

        public DateTime DateOfEvent { get; set; }

        public List<ParticipantBaseDTO> Participants { get; set; }

        public string Description { get; set; }
    }
}
