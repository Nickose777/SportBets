namespace SportBet.Services.DTOModels.Edit
{
    public class ParticipantEditDTO
    {
        public string NewParticipantName { get; set; }

        public string NewSportName { get; set; }

        public string NewCountryName { get; set; }

        public string OldParticipantName { get; set; }

        public string OldSportName { get; set; }

        public string OldCountryName { get; set; }
    }
}
