﻿using System.Collections.Generic;

namespace SportBet.Core.Entities
{
    public class ParticipantEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CountryId { get; set; }
        public virtual CountryEntity Country { get; set; }

        public int SportId { get; set; }
        public virtual SportEntity Sport { get; set; }

        public virtual ICollection<ParticipationEntity> Participations { get; set; }

        public virtual ICollection<TournamentEntity> Tournaments { get; set; }
    }
}
