using System;
using System.Collections.Generic;

namespace SportBet.Models.Base
{
    public class BetBaseModel
    {
        public string SportName { get; set; }

        public string TournamentName { get; set; }

        public DateTime DateOfEvent { get; set; }

        public List<ParticipantBaseModel> EventParticipants { get; set; }

        public string CoefficientDescription { get; set; }

        public string ClientPhoneNumber { get; set; }
    }
}
