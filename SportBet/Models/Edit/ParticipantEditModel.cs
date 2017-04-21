using SportBet.Models.Base;

namespace SportBet.Models.Edit
{
    public class ParticipantEditModel : ParticipantBaseModel
    {
        public string NewParticipantName { get; set; }

        public string NewSportName { get; set; }

        public string NewCountryName { get; set; }
    }
}
