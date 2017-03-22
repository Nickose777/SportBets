using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Core.Models
{
    public class SportEntity
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<TournamentEntity> Tournaments { get; set; }
        public virtual ICollection<ParticipantEntity> Participants { get; set; }
    }
}
