using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Core.Entities
{
    public class EventEntity
    {
        public int Id { get; set; }
        public DateTime DateOfEvent { get; set; }
        public string Notes { get; set; }

        public int TournamentId { get; set; }
        public virtual TournamentEntity Tournament { get; set; }

        public virtual ICollection<CoefficientEntity> Coefficients { get; set; }
        public virtual ICollection<ParticipationEntity> Participations { get; set; }
    }
}
