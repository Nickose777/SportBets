using System;

namespace SportBet.Services.DTOModels.Create
{
    public class EventCreateDTO
    {
        public string SportName { get; set; }

        public string TournamentName { get; set; }

        public DateTime DateOfTournamentStart { get; set; }

        public DateTime DateOfEvent { get; set; }

        public string Notes { get; set; }
    }
}
