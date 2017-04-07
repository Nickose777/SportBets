using System.Collections.Generic;

namespace SportBet.Core.Entities
{
    public class CoefficientEntity
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public decimal Value { get; set; }

        public bool? Win { get; set; }

        public int EventId { get; set; }
        public virtual EventEntity Event { get; set; }

        public virtual ICollection<BetEntity> Bets { get; set; }
    }
}
