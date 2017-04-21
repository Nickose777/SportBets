using System.Collections.Generic;
using SportBet.Models.Base;

namespace SportBet.Models.Extra
{
    public class ParticipantTournamentModel : ParticipantBaseModel
    {
        public List<TournamentBaseModel> Tournaments { get; set; }
    }
}
