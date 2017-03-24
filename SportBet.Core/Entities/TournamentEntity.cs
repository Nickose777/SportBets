using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Core.Entities
{
    public class TournamentEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfStart { get; set; }

        public int SportId { get; set; }
        public virtual SportEntity Sport { get; set; }

        public virtual ICollection<EventEntity> Events { get; set; }
    }
}
