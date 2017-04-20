using System.Collections.Generic;

namespace SportBet.Core.Entities
{
    public class SportEntity
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public bool IsDual { get; set; }

        public virtual ICollection<TournamentEntity> Tournaments { get; set; }

        public virtual ICollection<ParticipantEntity> Participants { get; set; }
    }
}
