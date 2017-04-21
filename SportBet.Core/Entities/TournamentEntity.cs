using System;
using System.Collections.Generic;

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

        public virtual ICollection<ParticipantEntity> Participants { get; set; }
    }
}
