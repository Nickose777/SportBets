using System.Collections.Generic;
using SportBet.Services.DTOModels.Base;

namespace SportBet.Services.DTOModels.Extra
{
    public class ParticipantTournamentDTO : ParticipantBaseDTO
    {
        public List<TournamentBaseDTO> Tournaments { get; set; }
    }
}
