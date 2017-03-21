using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Core.Models
{
    public class CoefficientEntity
    {
        public int Id { get; set; }

        public string Description { get; set; }
        public decimal Value { get; set; }
        public bool? Win { get; set; }

        public int EventID { get; set; }
        public virtual EventEntity Event { get; set; }

        public virtual ICollection<BetEntity> Bets { get; set; }
    }
}
