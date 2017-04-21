using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Models.Create
{
    public class EventCreateModel
    {
        public string SportName { get; set; }

        public string TournamentName { get; set; }

        public DateTime DateOfTournamentStart { get; set; }

        public DateTime DateOfEvent { get; set; }

        public string Notes { get; set; }
    }
}
