using System;
using System.Collections.Generic;

namespace SportBet.Services.DTOModels.Base
{
    public class BetBaseDTO
    {
        public string SportName { get; set; }

        public string TournamentName { get; set; }

        public DateTime DateOfEvent { get; set; }

        public List<ParticipantBaseDTO> EventParticipants { get; set; }

        public string CoefficientDescription { get; set; }

        public string ClientPhoneNumber { get; set; }
    }
}
