using SportBet.Services.DTOModels.Base;

namespace SportBet.Services.DTOModels.Edit
{
    public class ParticipantEditDTO : ParticipantBaseDTO
    {
        public string OldParticipantName { get; set; }

        public string OldSportName { get; set; }

        public string OldCountryName { get; set; }
    }
}
