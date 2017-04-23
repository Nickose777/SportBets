using System;
using System.Collections.Generic;

namespace SportBet.Models.Base
{
    public class CoefficientBaseModel
    {
        public string SportName { get; set; }

        public string TournamentName { get; set; }

        public DateTime DateOfEvent { get; set; }

        public List<ParticipantBaseModel> Participants { get; set; }

        public string Description { get; set; }
    }
}
