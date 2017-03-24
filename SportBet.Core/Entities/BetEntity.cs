using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBet.Core.Entities
{
    public class BetEntity
    {
        public DateTime RegistrationDate { get; set; }
        public decimal Sum { get; set; }

        public int ClientId { get; set; }
        public int CoefficientId { get; set; }
        public int BookmakerId { get; set; }

        public virtual ClientEntity Client { get; set; }
        public virtual CoefficientEntity Coefficient { get; set; }
        public virtual BookmakerEntity Bookmaker { get; set; }
    }
}
