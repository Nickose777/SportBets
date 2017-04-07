using System.Collections.Generic;

namespace SportBet.Core.Entities
{
    public class CountryEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ParticipantEntity> Participants { get; set; }
    }
}
