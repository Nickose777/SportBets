using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Core.Models
{
    public class ParticipationEntity
    {
        public short? Position { get; set; }

        public int EventId { get; set; }
        public int ParticipantId { get; set; }

        public virtual EventEntity Event { get; set; }
        public virtual ParticipantEntity Participant { get; set; }
    }
}
