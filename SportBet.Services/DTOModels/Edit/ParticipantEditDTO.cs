using SportBet.Services.DTOModels.Base;

namespace SportBet.Services.DTOModels.Edit
{
    public class ParticipantEditDTO : ParticipantBaseDTO
    {
        public string NewParticipantName { get; set; }

        public string NewSportName { get; set; }

        public string NewCountryName { get; set; }
    }
}
