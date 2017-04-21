using System;

namespace SportBet.Services.DTOModels.Base
{
    public class TournamentBaseDTO
    {
        public string Name { get; set; }

        public string SportName { get; set; }

        public DateTime DateOfStart { get; set; }
    }
}
