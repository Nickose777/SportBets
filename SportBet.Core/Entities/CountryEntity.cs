using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Core.Entities
{
    public class CountryEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ParticipantEntity> Participants { get; set; }
    }
}
