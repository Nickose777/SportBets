using System;
using System.Collections.Generic;

namespace SportBet.Models.Base
{
    public class EventBaseModel
    {
        public string SportName { get; set; }

        public string TournamentName { get; set; }

        public DateTime DateOfTournamentStart { get; set; }

        public List<ParticipantBaseModel> Participants { get; set; }
    }
}
