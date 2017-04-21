using System;
using System.Collections.Generic;
using SportBet.Models.Base;

namespace SportBet.Models.Edit
{
    public class TournamentEditModel : TournamentBaseModel
    {
        public string NewName { get; set; }

        public DateTime NewDateOfStart { get; set; }

        public List<ParticipantBaseModel> Participants { get; set; }
    }
}
