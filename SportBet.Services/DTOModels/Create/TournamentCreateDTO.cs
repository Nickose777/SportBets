using System;

namespace SportBet.Services.DTOModels.Create
{
    public class TournamentCreateDTO
    {
        public string Name { get; set; }

        public DateTime DateOfStart { get; set; }

        public string SportName { get; set; }
    }
}
