using System;

namespace SportBet.Services.DTOModels.Edit
{
    public class TournamentEditDTO
    {
        public string OldName { get; set; }

        public string OldSportName { get; set; }

        public DateTime OldDateOfStart { get; set; }

        public string NewName { get; set; }

        public string NewSportName { get; set; }

        public DateTime NewDateOfStart { get; set; }
    }
}
